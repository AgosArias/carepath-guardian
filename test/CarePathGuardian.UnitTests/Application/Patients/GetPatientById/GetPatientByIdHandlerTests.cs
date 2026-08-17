using CarePathGuardian.Application.Patients.GetPatientById;
using CarePathGuardian.Domain.Patients;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Patients.GetPatientById
{
    public class GetPatientByIdHandlerTests
    {
        [Fact]
		public async Task Handle_WhenPatientExists_ShouldReturnPatient()
		{
			var patient = new Patient(
				"PAT-001",
				"Ana",
				"Garcia",
				new DateOnly(1995, 5, 20),
				"ana@example.com",
				null);

				var repository = new FakePatientRepository
				{
					PatientToReturn = patient
				};

				var handler = new GetPatientByIdHandler(repository);
				var query = new GetPatientByIdQuery(patient.Id);

				var result = await handler.Handle(query);

				Assert.NotNull(result);
				Assert.Equal(patient.Id, result.Id);
				Assert.Equal("Ana", result.FirstName);
		}

		[Fact]
		public async Task Handle_WhenPatientDoesNotExist_ShouldReturnNull()
		{
			var repository = new FakePatientRepository
			{
				PatientToReturn = null
			};

			var handler = new GetPatientByIdHandler(repository);
			var query = new GetPatientByIdQuery(Guid.NewGuid());

			var result = await handler.Handle(query);

			Assert.Null(result);
		}
    }
}