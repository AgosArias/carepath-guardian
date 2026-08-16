using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.Application.Patients.CreatePatient;

public class CreatePatientHandler
{
	private readonly IPatientRepository _patientRepository;

	public CreatePatientHandler(IPatientRepository patientRepository)
	{
		_patientRepository = patientRepository;
	}

	public async Task Handle(CreatePatientCommand command)
	{
		var patient = new Patient(
            command.ExternalId,
            command.FirstName,
            command.LastName,
            command.DateOfBirth,
            command.Email,
            command.Phone);

		await _patientRepository.AddAsync(patient);
	}


}