using CarePathGuardian.Application.DataQualityIssues.GetAllDataQualityIssues;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.DataQualityIssues;
public class GetAllDataQualityIssuesHandlerTests
{
	[Fact]
	public async Task GetAllAsync_WhenPageSizeIsTwo_ShouldReturnTwoIssues()
	{
		var repository = new FakeDataQualityIssueRepositor();

		repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE1", "Issue 1", IssueSeverity.High));

        repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE2", "Issue 2", IssueSeverity.Medium));

        repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE3", "Issue 3", IssueSeverity.Low));

		var handler = new GetAllDataQualityIssuesHandler(repository);

		var query = new GetAllDataQualityIssuesQuery(null, null, 1, 2);
		var results = await handler.Handle(query);
		Assert.Equal(2, results.Count);
	}
	[Fact]
	public async Task GetAllAsync_WhenRequestingSecondPage_ShouldSkipFirstPage()
	{
        var repository = new FakeDataQualityIssueRepositor();

        var issue1 = new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE1", "Issue 1", IssueSeverity.High);

        var issue2 = new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE2", "Issue 2", IssueSeverity.High);

        var issue3 = new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE3", "Issue 3", IssueSeverity.High);


        repository.Issues.Add(issue1);
        repository.Issues.Add(issue2);
        repository.Issues.Add(issue3);

        var handler = new GetAllDataQualityIssuesHandler(repository);

        var query = new GetAllDataQualityIssuesQuery(
            null,
            null,
            2,
            2);

        var result = await handler.Handle(query);

        Assert.Single(result);
	}
	[Fact]
	public async Task GetAllAsync_WhenFilteringAndPaginating_ShouldApplyBoth()
	{
        var repository = new FakeDataQualityIssueRepositor();

        repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE1", "High 1", IssueSeverity.High));

        repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE2", "Low", IssueSeverity.Low));

        repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE3", "High 2", IssueSeverity.High));

        repository.Issues.Add(new DataQualityIssue(
            "Referral", Guid.NewGuid(), "RULE4", "High 3", IssueSeverity.High));

        var handler = new GetAllDataQualityIssuesHandler(repository);

        var query = new GetAllDataQualityIssuesQuery(
            IssueStatus.Open,
            IssueSeverity.High,
            1,
            2);

        var result = await handler.Handle(query);

        Assert.Equal(2, result.Count);

        Assert.All(
            result,
            issue =>
            {
                Assert.Equal(IssueStatus.Open, issue.Status);
                Assert.Equal(IssueSeverity.High, issue.Severity);
            });
	}
}