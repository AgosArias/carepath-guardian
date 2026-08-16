using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.Infrastructure.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
	private readonly CarePathGuardianDbContext _dbContext;

	public PatientRepository(CarePathGuardianDbContext dbContext)
	{
		_dbContext = dbContext;
	}
    public async Task AddAsync(Patient patient)
	{
		await _dbContext.Patients.AddAsync(patient);
		await _dbContext.SaveChangesAsync();
	}

    public async Task<Patient?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Patients.FindAsync(id);
	}
}