using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.Application.DataQualityIssues.ResolveDataQualityIssue;
public class ResolveDataQualityIssueHandler
{
	private readonly IDataQualityIssueRepository _dataQualityIssueRepository;

	public ResolveDataQualityIssueHandler(IDataQualityIssueRepository dataQualityIssueRepository)
	{
		_dataQualityIssueRepository = dataQualityIssueRepository;
	}

	public async Task<DataQualityIssue?> Handle(ResolveDataQualityIssueCommand command)
	{
		var issue = await _dataQualityIssueRepository.GetByIdAsync(command.Id);

		if(issue is null)
			return null;

		issue.Resolve(command.ResolutionNotes);

		await _dataQualityIssueRepository.UpdateAsync(issue);
		
		return issue;
	}
}