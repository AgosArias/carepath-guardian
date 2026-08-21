using CarePathGuardian.Application.DataQualityIssues.EvaluateReferralDataQuality;
using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.DataQualityIssues;
public class EvaluateReferralDataQualityHandlerTests
{
	[Fact]
	public async Task Handle_WhenReferralHasQualityIssue_ShouldSaveIssue()
	{
		Guid patientId = Guid.NewGuid();

		var referral = new Referral(
			patientId,
			DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-20)),
			"",
			"",
			ReferralStatus.Pending,
			ReferralPriority.Low);

		var appointments = new List<Appointment>();

		var repository = new FakeDataQualityIssueRepositor();
		var handler = new EvaluateReferralDataQualityHandler(repository);

		await handler.Handle(referral, appointments);

		Assert.NotEmpty(repository.Issues);
	}

	[Fact]
	public async Task Handle_WhenReferralHasNoQualityIssues_ShouldNotSaveIssue()
	{
		Guid patientId = Guid.NewGuid();

		var referral = new Referral(
			patientId,
			DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)),
			"",
			"",
			ReferralStatus.Pending,
			ReferralPriority.Low);

		var appointments = new List<Appointment>();

		var repository = new FakeDataQualityIssueRepositor();
		var handler = new EvaluateReferralDataQualityHandler(repository);

		await handler.Handle(referral, appointments);

		Assert.Empty(repository.Issues);
	}
	[Fact]
	public async Task Handle_WhenMultipleQualityIssuesExist_ShouldSaveAllIssues()
	{
		Guid patientId = Guid.NewGuid();

		var referral = new Referral(
			patientId,
			DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-20)),
			"",
			"",
			ReferralStatus.Pending,
			ReferralPriority.Low);

		var appointment = new Appointment(
			referral.Id,
			DateTime.UtcNow.AddDays(-25),
			AppointmentStatus.Completed,
			null
		);

		var appointments = new List<Appointment>(){appointment};

		var repository = new FakeDataQualityIssueRepositor();
		var handler = new EvaluateReferralDataQualityHandler(repository);

		await handler.Handle(referral, appointments);

		Assert.Equal(2, repository.Issues.Count);
	}
}
