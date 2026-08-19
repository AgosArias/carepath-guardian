using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.Application.Appointments.CreateAppointment;
public class CreateAppointmentHandler
{
	private readonly IAppointmentRepository _appointmentRepository;

	public CreateAppointmentHandler(IAppointmentRepository appointmentRepository)
	{
		_appointmentRepository = appointmentRepository;
	}

	public async Task<Appointment> Handle(CreateAppointmentCommand command)
	{
		var appointment = new Appointment(
			command.referralId,
			command.scheduledAtUtc,
			command.status,
			command.cancellationReason
		);
		await _appointmentRepository.AddAsync(appointment);
		return appointment;
	}
}