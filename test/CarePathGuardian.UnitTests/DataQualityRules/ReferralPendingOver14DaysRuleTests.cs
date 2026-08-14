using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Referrals;
using System.Collections.Generic;

namespace CarePathGuardian.UnitTests.DataQualityRules;
public class ReferralPendingOver14DaysRuleTests
{
	[Fact]
	public void Evaluate_WhenReferralPendingOver14DaysWithoutAppointment_ShouldReturnIssue()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Pending, ReferralPriority.Low);
		var appointments = new List<Appointment>();

		var rule = new ReferralPendingOver14DaysRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.NotNull(result);
		Assert.Equal("REFERRAL_PENDING_OVER_14_DAYS", result.RuleCode);
		Assert.Equal(referral.Id, result.EntityId);
		Assert.Equal(IssueSeverity.High, result.Severity);
	}
	[Fact]
	public void Evaluate_WhenReferralPendingLessThan14Days_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Pending, ReferralPriority.Low);

		var appointments = new List<Appointment>();

		var rule = new ReferralPendingOver14DaysRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.Null(result);
	}
	
	[Fact]
	public void Evaluate_WhenReferralHasAppointment_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Pending, ReferralPriority.Low);

		Appointment appointment = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Scheduled, null);
		var appointments = new List<Appointment>(){appointment};

		var rule = new ReferralPendingOver14DaysRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.Null(result);
	}
	
	[Fact]
	public void Evaluate_WhenReferralIsNotPending_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));

		Referral referral = new Referral(id, referralDate,"","",
			ReferralStatus.Cancelled, ReferralPriority.Low);

		var appointments = new List<Appointment>();

		var rule = new ReferralPendingOver14DaysRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.Null(result);

	}
}