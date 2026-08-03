using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.UniTests.Patients;

public class PatientTests
{
    [Fact]
    public void Constructor_WhenDateOfBirthIsInFuture_ShouldThrowArgumentException()
    {
        var futureDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10));

        Action action = () => new Patient(
            "PAT-001",
            "Ana",
            "Garcia",
            futureDate,
            "ana@gmail.com",
            null
        );

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("dateBirth", exception.ParamName);

    }
}
