using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityIssues;


namespace CarePathGuardian.Domain.DataQualityRules;

public class AppointmentBeforeReferralRule
{
	public DataQualityIssue? Evaluate(Appointment appointment, Referral referral)
	{
		if(DateOnly.FromDateTime(appointment.ScheduledAtUtc) < referral.ReferralDate)
		{
			return new DataQualityIssue(
			"Appointment",
			appointment.Id,
			"APPOINTMENT_BEFORE_REFERRAL",
			"Appointment date is before referral date.",
			IssueSeverity.High);
		}
		return null;
	}
}
