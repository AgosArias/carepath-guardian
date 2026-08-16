using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.Application.Abstractions.Persistence;

public interface IPatientRepository
{
	Task AddAsync(Patient patient);	
	Task<Patient?> GetByIdAsync(Guid id);
}