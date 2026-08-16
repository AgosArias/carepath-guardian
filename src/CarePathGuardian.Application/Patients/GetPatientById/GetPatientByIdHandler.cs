using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.Application.Patients.GetPatientById;

public class GetPatientByIdHandler
{
	private readonly IPatientRepository _patientRepository;

	public GetPatientByIdHandler(IPatientRepository patientRepository)
	{
		_patientRepository = patientRepository;
	}

	public async Task<Patient?> Handle(GetPatientByIdQuery query)
	{
		return await _patientRepository.GetByIdAsync(query.Id);
	}
}
