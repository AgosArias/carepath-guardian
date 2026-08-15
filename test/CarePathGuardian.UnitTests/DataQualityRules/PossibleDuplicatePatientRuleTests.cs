using CarePathGuardian.Domain.Patients;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.UnitTests.DataQualityRules;

public class PossibleDuplicatePatientRuleTests
{
	[Fact]
	public void Evaluate_WhenPatientsMatchStrongly_ShouldReturnIssue()
	{
		var dateOfBirth = new DateOnly(1995,5,20);

		var patient1 = new Patient(
		"PAT-001",
		"Ana",
		"Garcia",
		dateOfBirth,
		"",
		"");

		var patient2 = new Patient(
		"PAT-003",
		"Ana",
		"Garcia",
		dateOfBirth,
		"",
		"");

		var rule = new PossibleDuplicatePatientRule();
		var results = rule.Evaluate(patient1,patient2);

		Assert.NotNull(results);
		Assert.Equal("POSSIBLE_DUPLICATE_PATIENT",results.RuleCode);
		Assert.Equal(patient1.Id, results.EntityId);
		Assert.Equal(IssueSeverity.Medium, results.Severity);
	}
	
	[Fact]
	public void Evaluate_WhenPatientsAreDifferent_ShouldReturnNull()
	{
		var dateOfBirth1 = new DateOnly(1995,5,20);
		var dateOfBirth2 = new DateOnly(1999,5,20);

		var patient1 = new Patient(
		"PAT-001",
		"Ana",
		"Garcia",
		dateOfBirth1,
		"",
		"");

		var patient2 = new Patient(
		"PAT-003",
		"Ana",
		"Garcia",
		dateOfBirth2,
		"",
		"");

		var rule = new PossibleDuplicatePatientRule();
		var results = rule.Evaluate(patient1,patient2);

		Assert.Null(results);
	}
	[Fact]
	public void Evaluate_WhenSamePatientIsCompared_ShouldReturnNull()
	{
		var dateOfBirth = new DateOnly(1995,5,20);

		var patient = new Patient(
		"PAT-001",
		"Ana",
		"Garcia",
		dateOfBirth,
		"",
		"");

		var rule = new PossibleDuplicatePatientRule();
		var results = rule.Evaluate(patient,patient);

		Assert.Null(results);
	}
	[Fact]
	public void Evaluate_WhenNamesDifferOnlyByCase_ShouldReturnIssue()
	{
		var dateOfBirth = new DateOnly(1995,5,20);

		var patient1 = new Patient(
		"PAT-001",
		"ana",
		"garcia",
		dateOfBirth,
		"",
		"");

		var patient2 = new Patient(
		"PAT-003",
		"Ana",
		"Garcia",
		dateOfBirth,
		"",
		"");

		var rule = new PossibleDuplicatePatientRule();
		var results = rule.Evaluate(patient1,patient2);

		Assert.NotNull(results);
		Assert.Equal("POSSIBLE_DUPLICATE_PATIENT",results.RuleCode);
		Assert.Equal(patient1.Id, results.EntityId);
		Assert.Equal(IssueSeverity.Medium, results.Severity);
	}
}
