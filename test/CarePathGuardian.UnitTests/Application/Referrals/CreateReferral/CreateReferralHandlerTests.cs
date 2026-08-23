using CarePathGuardian.Application.DataQualityIssues.EvaluateReferralDataQuality;
using CarePathGuardian.Application.Referrals.CreateReferral;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Referrals.CreateReferral;
public class CreateReferralHandlerTests
{
	[Fact]
	public async Task Handle_ShouldAddReferral()
	{
		Guid id = Guid.NewGuid();

		var repository = new FakeReferralRepository();
		var qualityHandler = new EvaluateReferralDataQualityHandler(new FakeDataQualityIssueRepositor());

		var handler = new CreateReferralHandle(repository, qualityHandler);

		var command = new CreateReferralCommand(id, 
		new DateOnly(2026, 5, 20),
		"",
		"",
		ReferralStatus.Pending,
		ReferralPriority.High
		);

		await handler.Handle(command);
		Assert.NotNull(repository.AddedReferral);
		Assert.Equal(id, command.patientId);
	}

	[Fact]
	public async Task Handle_WhenReferralHasQualityIssue_ShouldCreateIssue()
	{
		Guid id = Guid.NewGuid();
		var repository = new FakeReferralRepository();
		var issueRepository = new FakeDataQualityIssueRepositor();
		var qualityHandler = new EvaluateReferralDataQualityHandler(issueRepository);
		var handler = new CreateReferralHandle(repository, qualityHandler);

		var command = new CreateReferralCommand(id, 
		DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-20)),
		"",
		"",
		ReferralStatus.Pending,
		ReferralPriority.High
		);

		await handler.Handle(command);

		Assert.NotEmpty(issueRepository.Issues);
	}
}
