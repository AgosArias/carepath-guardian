using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.UnitTests.DataQualityRules;
public class AppointmentBeforeReferralRuleTests
{
	[Fact]
	public void Evaluate_WhenAppointmentIsBeforeReferral_ShouldReturnIssue()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(-20);

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Pending, ReferralPriority.Low);
		Appointment appointment = new Appointment(referral.Id, scheduledAtUtc, 
		AppointmentStatus.Scheduled, null);

		var rule = new AppointmentBeforeReferralRule();
		var result = rule.Evaluate(appointment, referral);
		Assert.NotNull(result);
		Assert.Equal("APPOINTMENT_BEFORE_REFERRAL", result.RuleCode);
		Assert.Equal(appointment.Id, result.EntityId);
		Assert.Equal(IssueSeverity.High, result.Severity);
	}

	[Fact]
	public void Evaluate_WhenAppointmentIsAfterReferral_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(-10);

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Pending, ReferralPriority.Low);
		Appointment appointment = new Appointment(referral.Id, scheduledAtUtc, 
		AppointmentStatus.Scheduled, null);

		var rule = new AppointmentBeforeReferralRule();
		var result = rule.Evaluate(appointment, referral);
		
		Assert.Null(result);
	}
	[Fact]
	public void Evaluate_WhenAppointmentIsOnSameDateAsReferral_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		DateTime scheduledAtUtc = referralDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Pending, ReferralPriority.Low);
		Appointment appointment = new Appointment(referral.Id, scheduledAtUtc, 
		AppointmentStatus.Scheduled, null);

		var rule = new AppointmentBeforeReferralRule();
		var result = rule.Evaluate(appointment, referral);
		Assert.Null(result);
	}
}
