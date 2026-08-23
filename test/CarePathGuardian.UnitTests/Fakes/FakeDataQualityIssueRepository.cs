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
    IssueSeverity? severity = null)
	{
		IEnumerable<DataQualityIssue> issues = Issues;
		if(status.HasValue)
			issues = issues.Where(i => i.Status == status.Value);
		if(severity.HasValue)
			issues = issues.Where(i => i.Severity == severity.Value);
		return Task.FromResult(issues.ToList());
	} 
}