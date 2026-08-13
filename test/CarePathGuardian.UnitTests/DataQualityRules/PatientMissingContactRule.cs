using System;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.Patients;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.UnitTests.DataQualityRules
{
    public class PatientMissingContactRuleTest
    {
        [Fact]
		public void Evaluate_WhenPatientHasNoContact_ShouldReturnIssue()
		{
			var dateOfBirth = new DateOnly(1995,5,20);

			var patient = new Patient(
			"PAT-001",
			"Ana",
			"Garcia",
			dateOfBirth,
			"",
			"");

			PatientMissingContactRule dqr = new PatientMissingContactRule();
			var results = dqr.Evaluate(patient);

			Assert.NotNull(results);
			Assert.Equal("PATIENT_MISSING_CONTACT",results.RuleCode);
			Assert.Equal(patient.Id, results.EntityId);
			Assert.Equal(IssueSeverity.High, results.Severity);
		}

		[Fact]
		public void Evaluate_WhenPatientHasEmail_ShouldReturnNull()
		{
			var dateOfBirth = new DateOnly(1995,5,20);

			var patient = new Patient(
			"PAT-001",
			"Ana",
			"Garcia",
			dateOfBirth,
			"",
			"email@mail.com");

			PatientMissingContactRule dqr = new PatientMissingContactRule();
			var results = dqr.Evaluate(patient);

			Assert.Null(results);
		}
		[Fact]
		public void Evaluate_WhenPatientHasPhone_ShouldReturnNull()
		{
			var dateOfBirth = new DateOnly(1995,5,20);

			var patient = new Patient(
			"PAT-001",
			"Ana",
			"Garcia",
			dateOfBirth,
			"+353000000000",
			"");

			PatientMissingContactRule dqr = new PatientMissingContactRule();
			var result = dqr.Evaluate(patient);

			Assert.Null(result);
		}
    }
}