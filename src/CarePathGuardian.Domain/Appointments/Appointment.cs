using System;

namespace CarePathGuardian.Domain.Appointments;
public class Appointment
{
    public Guid Id {get; private set ;}	
	public Guid ReferralId {get; private set ;}
	public DateTime ScheduledAtUtc {get; private set ;}
	public AppointmentStatus Status {get; private set ;}
	public string? CancellationReason {get; private set ;}
	public DateTime CreatedAtUtc {get; private set ;}
	public DateTime UpdatedAtUtc {get; private set ;}
	private Appointment(){}
	public Appointment(
		Guid referralId,
		DateTime scheduledAtUtc,
		AppointmentStatus status,
		string? cancellationReason
		)
	{
		if(referralId == Guid.Empty){throw new ArgumentException("Referral id is required.", nameof(referralId));}
		Id = Guid.NewGuid();
		ReferralId = referralId;
		ScheduledAtUtc = scheduledAtUtc;
		Status = status;
		CancellationReason = string.IsNullOrWhiteSpace(cancellationReason)?null:cancellationReason.Trim();
		CreatedAtUtc = DateTime.UtcNow;
		UpdatedAtUtc = DateTime.UtcNow;
	}
}