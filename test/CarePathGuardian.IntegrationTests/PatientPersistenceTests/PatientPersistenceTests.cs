using CarePathGuardian.Infrastructure.Persistence;
using CarePathGuardian.Domain.Patients;
using Microsoft.EntityFrameworkCore;

namespace CarePathGuardian.IntegrationTests.PatientPersistenceTests
{
    public class PatientPersistenceTests
    {
        [Fact]
		public async Task SavePatient_ShouldPersistPatient()
		{
			var connectionString = Environment.GetEnvironmentVariable(
				"CAREPATH_TEST_CONNECTION");

			var options = new DbContextOptionsBuilder<CarePathGuardianDbContext>()
			.UseNpgsql(connectionString)
			.Options;

			await using var context = new CarePathGuardianDbContext(options);
			        var patient = new Patient(
            $"PAT-{Guid.NewGuid()}",
            "Ana",
            "Garcia",
            new DateOnly(1995, 5, 20),
            "ana@example.com",
            null);

			context.Patients.Add(patient);
			await context.SaveChangesAsync();

			var savedPatient = await context.Patients
			.FirstOrDefaultAsync(x=>x.Id == patient.Id);

			Assert.NotNull(savedPatient);
			Assert.Equal("Ana",savedPatient.FirstName);
		}
    }
}