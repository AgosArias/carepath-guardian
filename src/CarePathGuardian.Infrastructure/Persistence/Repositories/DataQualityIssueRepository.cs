using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.DataQualityIssues;

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
}