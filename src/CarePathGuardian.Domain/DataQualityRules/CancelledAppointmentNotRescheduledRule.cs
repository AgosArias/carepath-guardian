using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Referrals;	

namespace CarePathGuardian.Domain.DataQualityRules;
public class CancelledAppointmentNotRescheduledRule : IReferralDataQualityRule
{
	public DataQualityIssue? Evaluate (Referral referral,	IEnumerable<Appointment> appointments)
	{
		bool hasScheduledAppointment = appointments.Any(a => a.Status == AppointmentStatus.Scheduled &&
		a.ReferralId == referral.Id);
		foreach( var  appointment in appointments)
		{
			if(appointment.Status == AppointmentStatus.Cancelled && !hasScheduledAppointment)
			{
				return new DataQualityIssue(
				"Appointment",
				appointment.Id,
				"CANCELLED_APPOINTMENT_NOT_RESCHEDULED",
				"Cancelled appointment has not been rescheduled.",
				IssueSeverity.High);
			}	
		}
		return null;
	}
}
