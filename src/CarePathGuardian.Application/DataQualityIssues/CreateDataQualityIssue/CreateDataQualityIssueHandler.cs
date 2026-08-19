using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.Application.DataQualityIssues.CreateDataQualityIssue;
public class CreateDataQualityIssueHandler
{
	private readonly IDataQualityIssueRepository _dataQualityIssueRepository;

	public CreateDataQualityIssueHandler(IDataQualityIssueRepository dataQualityIssueRepository)
	{
		_dataQualityIssueRepository = dataQualityIssueRepository;
	}

	public async Task<DataQualityIssue> Handle(CreateDataQualityIssueCommand command)
	{
		var issue = new DataQualityIssue(
			command.EntityType,
            command.EntityId,
            command.RuleCode,
            command.Description,
            command.Severity
		);

		await _dataQualityIssueRepository.AddAsync(issue);
		return issue;
	}
}