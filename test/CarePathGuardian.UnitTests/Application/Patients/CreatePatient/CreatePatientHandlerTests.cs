using CarePathGuardian.Application.Patients.CreatePatient;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Patients.CreatePatient
{
	public class CreatePatientHandlerTests
	{
		[Fact]
		public async Task Handle_ShouldAddPatient()
		{
			var repository = new FakePatientRepository();
			var handler = new CreatePatientHandler(repository);

			var command = new CreatePatientCommand(
				"PAT-001",
				"Ana",
				"Garcia",
				new DateOnly(1995, 5, 20),
				"ana@example.com",
				null);

			await handler.Handle(command);
			Assert.NotNull(repository.AddedPatient);
			Assert.Equal("Ana", repository.AddedPatient.FirstName);
			Assert.Equal("Garcia", repository.AddedPatient.LastName);
		}
	
	
	}
}