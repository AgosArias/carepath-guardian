using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Patients;

namespace CarePathGuardian.Domain.DataQualityRules;

public class PatientMissingContactRule
{
	public DataQualityIssue? Evaluate(Patient patient)
	{
		if(patient.Email is null && patient.PhoneNumber is null)
		{
			return new DataQualityIssue(
				"Patient",
				patient.Id,
				"PATIENT_MISSING_CONTACT",
				"Patient has no email or phone number.",
				IssueSeverity.High);
		}
		return null;
	}
}