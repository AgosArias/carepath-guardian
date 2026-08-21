using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Referrals;

namespace CarePathGuardian.UnitTests.DataQualityRules;
public class CancelledAppointmentNotRescheduledRuleTests
{
	[Fact]
	public void Evaluate_WhenAppointmentIsCancelledAndNotRescheduled_ShouldReturnIssue()
	{
		Guid id = Guid.NewGuid();
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);
		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		var referral = new Referral(
            id,
            referralDate,
            "",
            "",
            ReferralStatus.Pending,
            ReferralPriority.Low);

		Appointment appointment1 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Cancelled, null);
		Appointment appointment2 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Cancelled, null);
		Appointment appointment3 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Completed, null);
		var appointments = new List<Appointment>(){appointment1,appointment2,appointment3};
		
		var rule = new CancelledAppointmentNotRescheduledRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.NotNull(result);
		Assert.Equal("CANCELLED_APPOINTMENT_NOT_RESCHEDULED", result.RuleCode);
		Assert.Equal(appointment1.Id, result.EntityId);
		Assert.Equal(IssueSeverity.High, result.Severity);
	}
	[Fact]
	public void Evaluate_WhenCancelledAppointmentHasNewAppointment_ShouldReturnNull()
	{
		
		Guid id = Guid.NewGuid();
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);

		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		var referral = new Referral(
            id,
            referralDate,
            "",
            "",
            ReferralStatus.Pending,
            ReferralPriority.Low);

		Appointment appointment1 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Cancelled, null);
		Appointment appointment2 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Scheduled, null);
		Appointment appointment3 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Completed, null);
		var appointments = new List<Appointment>(){appointment1,appointment2,appointment3};
		
		var rule = new CancelledAppointmentNotRescheduledRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.Null(result);
	}
	
	[Fact]
	public void Evaluate_WhenNoAppointmentIsCancelled_ShouldReturnNull()
	{
		
		Guid id = Guid.NewGuid();
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);

		var referralDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15));
		var referral = new Referral(
            id,
            referralDate,
            "",
            "",
            ReferralStatus.Pending,
            ReferralPriority.Low);

		Appointment appointment1 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Completed, null);
		Appointment appointment2 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Scheduled, null);
		Appointment appointment3 = new Appointment(referral.Id, scheduledAtUtc, AppointmentStatus.Completed, null);
		var appointments = new List<Appointment>(){appointment1,appointment2,appointment3};
		
		var rule = new CancelledAppointmentNotRescheduledRule();
		var result = rule.Evaluate(referral, appointments);

		Assert.Null(result);
	}
}