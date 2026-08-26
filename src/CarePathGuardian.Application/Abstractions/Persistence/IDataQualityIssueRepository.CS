using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.Abstractions.Persistence;

public interface IDataQualityIssueRepository
{
	Task AddAsync(DataQualityIssue issue);
	Task<DataQualityIssue?> GetByIdAsync(Guid id);
	Task<List<DataQualityIssue>> GetAllAsync(IssueStatus? status = null, 
	IssueSeverity? severity = null, 
	int page = 1,
    int pageSize = 20);
	Task UpdateAsync(DataQualityIssue issue);
}