using CarePathGuardian.Domain.Appointments;

namespace CarePathGuardian.Application.Appointments.CreateAppointment;

public sealed record CreateAppointmentCommand(
		Guid referralId,
	DateTime scheduledAtUtc,
	AppointmentStatus status,
	string? cancellationReason	
);