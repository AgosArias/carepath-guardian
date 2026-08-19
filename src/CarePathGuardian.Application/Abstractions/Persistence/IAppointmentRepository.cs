using CarePathGuardian.Domain.Appointments;

namespace CarePathGuardian.Application.Abstractions.Persistence;

public interface IAppointmentRepository
{
	Task AddAsync(Appointment appointment);
	Task<Appointment?> GetByIdAsync(Guid id);
}