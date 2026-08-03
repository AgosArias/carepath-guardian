using System.Security.Cryptography.X509Certificates;
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

    [Fact]
    public void Constructor_WhenExternalReferenceIsEmpty_ShouldThrowArgumentException()
    {
        Action action = () => new Patient(
            "",
            "Ana",
            "Garcia",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)),
            "ana@gmail.com",
            null
        );
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("externalReference", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenFirstNameIsEmpty_ShouldThrowArgumentException()
    {
        Action action = () => new Patient(
            "PAT-001",
            "",
            "Garcia",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)),
            "ana@gmail.com",
            null
        );
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("firstName", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenLastNameIsEmpty_ShouldThrowArgumentException()
    {
        Action action = () => new Patient(
            "PAT-001",
            "Ana",
            "",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)),
            "ana@gmail.com",
            null
        );
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("lastName", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenDataIsValid_ShouldCreatePatient()
    {        
        var dateOfBirth = new DateOnly(1995,5,20);

        var patient = new Patient(
        "PAT-001",
        "Ana",
        "Garcia",
        dateOfBirth,
        "ana@gmail.com",
        "+353871234567");
    
    Assert.NotEqual(Guid.Empty,patient.Id);
    Assert.Equal("PAT-001", patient.ExternalReference);
    Assert.Equal("Ana",patient.FirstName);
    Assert.Equal("Garcia",patient.LastName);
    Assert.Equal(dateOfBirth,patient.DateOfBirth);
    Assert.Equal("ana@gmail.com",patient.Email);
    Assert.Equal("+353871234567",patient.PhoneNumber);
    Assert.True(patient.CreatedAtUtc <= DateTime.UtcNow);
    Assert.True(patient.UpdatedAtUtc <= DateTime.UtcNow);    
    }

}
