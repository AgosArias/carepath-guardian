using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.Abstractions.Persistence;

public interface IDataQualityIssueRepository
{
	Task AddAsync(DataQualityIssue issue);
	Task<DataQualityIssue?> GetByIdAsync(Guid id);
}