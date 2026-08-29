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
	int pageSize = 20, string? sort = null)
	{
		IEnumerable<DataQualityIssue> issues = Issues;
		if(status.HasValue)
			issues = issues.Where(i => i.Status == status.Value);
		if(severity.HasValue)
			issues = issues.Where(i => i.Severity == severity.Value);
		DateTime nowUtc = DateTime.UtcNow;
		if (sort == "priority")
			issues = issues.OrderByDescending(i => i.GetPriority(nowUtc));
		else if (sort == "oldest")
			issues = issues.OrderBy(i => i.DetectedAtUtc);
		else if (sort == "severity")
			issues = issues.OrderByDescending(i => i.Severity);
		else
			issues = issues.OrderByDescending(i => i.DetectedAtUtc);
		return Task.FromResult(issues
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.ToList());
	} 

	public Task UpdateAsync(DataQualityIssue issue)
	{
		return Task.CompletedTask;
	}

	public Task<bool> ExistsOpenAsync(
    string entityType,
    Guid entityId,
    string ruleCode)
	{
		IEnumerable<DataQualityIssue> issues = Issues;
		return Task.FromResult(issues
		.Any(i=> i.Status == IssueStatus.Open &&
		i.EntityId == entityId 
		&& i.EntityType == entityType 
		&& i.RuleCode== ruleCode));
	}
}