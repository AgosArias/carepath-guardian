using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Appointments;

namespace CarePathGuardian.Infrastructure.Persistence.Repositories;
public class AppointmentRepository : IAppointmentRepository
{
	private readonly CarePathGuardianDbContext _DbContext;

	public AppointmentRepository(CarePathGuardianDbContext dbContext)
	{
		_DbContext = dbContext;
	}
    public async Task AddAsync(Appointment appointment)
	{
		await _DbContext.Appointments.AddAsync(appointment);
		await _DbContext.SaveChangesAsync();
	}
	public async Task<Appointment?> GetByIdAsync(Guid id)
	{
		return await _DbContext.Appointments.FindAsync(id);
	}
}
