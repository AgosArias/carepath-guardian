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

	public async Task<List<DataQualityIssue>> GetAllAsync(IssueStatus? status = null, IssueSeverity? severity = null)
	{
		var query = _dbContext.DataQualityIssues.AsNoTracking();

		if(status.HasValue)
			query = query.Where(i => i.Status == status.Value);
		if(severity.HasValue)
			query = query.Where(i => i.Severity == severity.Value);

		return await query.ToListAsync();
	}

	public async Task UpdateAsync(DataQualityIssue issue)
	{
		await _dbContext.SaveChangesAsync();
	}
}