using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.DataQualityIssues;
using Microsoft.EntityFrameworkCore;

namespace CarePathGuardian.Infrastructure.Persistence.Repositories;
public class DataQualityIssueRepository : IDataQualityIssueRepository
{
	private readonly CarePathGuardianDbContext _dbContext;

	public DataQualityIssueRepository(CarePathGuardianDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task AddAsync(DataQualityIssue issue)
	{
		await _dbContext.DataQualityIssues.AddAsync(issue);
		await _dbContext.SaveChangesAsync();
	}
	public async Task<DataQualityIssue?> GetByIdAsync(Guid id)
	{
		return await _dbContext.DataQualityIssues.FindAsync(id);
	}

	public async Task<List<DataQualityIssue>> GetAllAsync(IssueStatus? status = null, 
	IssueSeverity? severity = null, 
	int page = 1,
	int pageSize = 20, string? sort = null)
	{
		var query = _dbContext.DataQualityIssues.AsNoTracking();

		if(status.HasValue)
			query = query.Where(i => i.Status == status.Value);
		if(severity.HasValue)
			query = query.Where(i => i.Severity == severity.Value);

		DateTime nowUtc = DateTime.UtcNow;
		DateTime priorityCutoff = nowUtc.AddDays(-30);
		if (sort == "priority")
		{
			query = query
				.OrderByDescending(i =>
					i.Severity == IssueSeverity.High &&
					i.DetectedAtUtc < priorityCutoff ? 3 :
					i.Severity == IssueSeverity.High ? 2 :
					i.Severity == IssueSeverity.Medium &&
					i.DetectedAtUtc < priorityCutoff ? 2 :
					i.Severity == IssueSeverity.Medium ? 1 :
					0)
				.ThenBy(i => i.DetectedAtUtc);
		}
		else if(sort == "oldest")
			query = query.OrderBy(i => i.DetectedAtUtc);
		else if(sort == "severity")
			query = query.OrderByDescending(i => i.Severity);
		else
			query = query.OrderByDescending(i => i.DetectedAtUtc);
		return await query
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.ToListAsync();
	}

	public async Task UpdateAsync(DataQualityIssue issue)
	{
		await _dbContext.SaveChangesAsync();
	}
}