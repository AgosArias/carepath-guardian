using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.UnitTests.Fakes;
public class FakePatientRepository : IPatientRepository
{
    public Patient? AddedPatient {get; private set;}

	public Task AddAsync(Patient patient)
	{
		AddedPatient = patient;
		return Task.CompletedTask;
	}

	public Patient? PatientToReturn {get; set;}

	public Task<Patient?> GetByIdAsync(Guid id)
	{
		return Task.FromResult(PatientToReturn);
	}
}
