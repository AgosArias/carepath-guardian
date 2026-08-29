using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityIssues;


namespace CarePathGuardian.Domain.DataQualityRules;

public class AppointmentBeforeReferralRule : IReferralDataQualityRule
{
	public string RuleCode => "APPOINTMENT_BEFORE_REFERRAL";
	public DataQualityIssue? Evaluate(Referral referral, IEnumerable<Appointment> appointments)
	{
		foreach( var appointment in appointments)
		{
			if(DateOnly.FromDateTime(appointment.ScheduledAtUtc) < referral.ReferralDate)
			{
				return new DataQualityIssue(
				"Appointment",
				appointment.Id,
				RuleCode,
				"Appointment date is before referral date.",
				IssueSeverity.High);
			}
		}
		return null;
	}
}
