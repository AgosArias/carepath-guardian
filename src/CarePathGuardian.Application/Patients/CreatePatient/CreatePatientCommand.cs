using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.Application.Patients.CreatePatient;

public sealed record CreatePatientCommand(
    string ExternalId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string? Email,
    string? Phone);