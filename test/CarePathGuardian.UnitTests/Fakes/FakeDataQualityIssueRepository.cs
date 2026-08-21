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

	public void Dispose(){}
}