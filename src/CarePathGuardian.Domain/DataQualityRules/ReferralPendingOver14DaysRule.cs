using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Referrals;

namespace CarePathGuardian.Domain.DataQualityRules;

public class ReferralPendingOver14DaysRule : IReferralDataQualityRule
{
	public string RuleCode => "REFERRAL_PENDING_OVER_14_DAYS";
	public DataQualityIssue? Evaluate(Referral referral, IEnumerable<Appointment> appointments)
	{

		bool isPendingOver14Days = referral.Status == ReferralStatus.Pending && 
		referral.ReferralDate < DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-14));


		bool hasScheduledAppointment = appointments.Any(
			a => a.ReferralId == referral.Id && 
			a.Status == AppointmentStatus.Scheduled);

		if(!hasScheduledAppointment && isPendingOver14Days)
		{
			return new DataQualityIssue(
		"Referral",
		referral.Id,
		RuleCode,
		"Referral has been pending for more than 14 days without an appointment.",
		IssueSeverity.High);
		}
		return null;
	}
}