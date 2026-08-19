using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.DataQualityIssues.GetDataQualityIssueById;
public class GetDataQualityIssueByIdHandler
{
	private readonly IDataQualityIssueRepository _dataQualityIssueRepository;

	public GetDataQualityIssueByIdHandler(IDataQualityIssueRepository dataQualityIssueRepository)
	{
		_dataQualityIssueRepository = dataQualityIssueRepository;
	}

	public async Task<DataQualityIssue?> Handle(GetDataQualityIssueByIdQuery query)
	{
		return await _dataQualityIssueRepository.GetByIdAsync(query.Id);
	}
}