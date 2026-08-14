using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.DataQualityIssues;
using System.Collections.Generic;
	

namespace CarePathGuardian.Domain.DataQualityRules;
public class CancelledAppointmentNotRescheduledRule
{
	public DataQualityIssue? Evaluate (Appointment appointment,	IEnumerable<Appointment> appointments)
	{
		bool hasAppointments = appointments.Any(a => a.Status == AppointmentStatus.Scheduled &&
		a.ReferralId == appointment.ReferralId);
		if(appointment.Status == AppointmentStatus.Cancelled && !hasAppointments)
		{
			return new DataQualityIssue(
			"Appointment",
			appointment.Id,
			"CANCELLED_APPOINTMENT_NOT_RESCHEDULED",
			"Cancelled appointment has not been rescheduled.",
			IssueSeverity.High);
		}	
		return null;
	}
}
