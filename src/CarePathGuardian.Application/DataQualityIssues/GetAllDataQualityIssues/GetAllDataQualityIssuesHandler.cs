using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.DataQualityIssues.GetAllDataQualityIssues;
public class GetAllDataQualityIssuesHandler
{
	private readonly IDataQualityIssueRepository _dataQualityIssueRepository;

	public GetAllDataQualityIssuesHandler(IDataQualityIssueRepository dataQualityIssueRepository)
	{
		_dataQualityIssueRepository = dataQualityIssueRepository;
	}

	public async Task<List<DataQualityIssue>> Handle(GetAllDataQualityIssuesQuery query)
	{
		return await _dataQualityIssueRepository.GetAllAsync();
	}
}