using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.Application.Appointments.GetAppointmentById;
public class GetAppointmentByIdHandler
{
	private readonly IAppointmentRepository _appointmentRepository;

	public GetAppointmentByIdHandler(IAppointmentRepository appointmentRepository)
	{
		_appointmentRepository = appointmentRepository;
	}

	public async Task<Appointment?> Handle(GetAppointmentByIdQuery query)
	{
		return await _appointmentRepository.GetByIdAsync(query.Id);
	}
}