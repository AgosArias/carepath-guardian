using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.UnitTests.Fakes;
public class FakeDataQualityIssueRepositor : IDataQualityIssueRepository
{
	public List<DataQualityIssue> Issues {get; } = new();
	public Task AddAsync(DataQualityIssue issue)
	{
		Issues.Add(issue);
		return Task.CompletedTask;
	}
	public Task<DataQualityIssue?> GetByIdAsync(Guid id)
	{
		return Task.FromResult(
			Issues.FirstOrDefault(i => i.Id == id));
		
	}

	public Task<List<DataQualityIssue>> GetAllAsync(IssueStatus? status = null,
    IssueSeverity? severity = null, 
	int page = 1,
    int pageSize = 20)
	{
		IEnumerable<DataQualityIssue> issues = Issues;
		if(status.HasValue)
			issues = issues.Where(i => i.Status == status.Value);
		if(severity.HasValue)
			issues = issues.Where(i => i.Severity == severity.Value);
		issues = issues
        .OrderByDescending(i => i.DetectedAtUtc)
        .Skip((page - 1) * pageSize)
        .Take(pageSize);
		return Task.FromResult(issues.ToList());
	} 

	public Task UpdateAsync(DataQualityIssue issue)
	{
		return Task.CompletedTask;
	}
}