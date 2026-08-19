using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Application.Abstractions.Persistence;
namespace CarePathGuardian.UnitTests.Fakes;
public class FakeAppointmentRepository : IAppointmentRepository
{
	public Appointment? AddedAppointment {get; private set;}

	public Task AddAsync(Appointment appointment)
	{
		AddedAppointment = appointment;
		return Task.CompletedTask;
	}

	public Appointment? AppointmentToReturn {get; set;}

	public Task<Appointment?> GetByIdAsync(Guid id)
	{
		return Task.FromResult(AppointmentToReturn);
	}
}