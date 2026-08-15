using CarePathGuardian.Domain.Patients;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Domain.DataQualityRules;
public class PossibleDuplicatePatientRule
{
	public DataQualityIssue? Evaluate(
	Patient patient,
	Patient otherPatient)
	{
		if(patient.Id == otherPatient.Id)
			return null;

		bool firstNameEqual = string.Equals(patient.FirstName.Trim(), otherPatient.FirstName.Trim(),StringComparison.OrdinalIgnoreCase);
		bool lastNameEqual = string.Equals(patient.LastName.Trim(), otherPatient.LastName.Trim(),StringComparison.OrdinalIgnoreCase);
		bool dateOfBirthEqual = patient.DateOfBirth.Equals(otherPatient.DateOfBirth);
		if(firstNameEqual && lastNameEqual && dateOfBirthEqual)
		{
			return new DataQualityIssue(
			"Patient",
			patient.Id,
			"POSSIBLE_DUPLICATE_PATIENT",
			"Possible duplicate patient detected.",
			IssueSeverity.Medium);
		}
		return null;
	}
}
