using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.DataQualityRules;

namespace CarePathGuardian.UnitTests.DataQualityRules;
public class ReferralMissingProfessionalRuleTest
{

    [Fact]
	public void Evaluate_WhenReferralHasNoProfessional_ShouldReturnIssue()
	{
		Guid id = Guid.NewGuid();
		var referralDate = new DateOnly(1995,5,20);
		var referral = new Referral(
			id, 
			referralDate,
			"Clinic",
			"",
			ReferralStatus.Scheduled,
			ReferralPriority.Low);
		var rmpr = new ReferralMissingProfessionalRule();
		var result = rmpr.Evaluate(referral);

		Assert.NotNull(result);
		Assert.Equal("REFERRAL_MISSING_PROFESSIONAL", result.RuleCode);
		Assert.Equal(referral.Id, result.EntityId);
		Assert.Equal(IssueSeverity.Medium, result.Severity);
	}
	[Fact]
	public void Evaluate_WhenReferralHasProfessional_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var referralDate = new DateOnly(1995,5,20);
		var referral = new Referral(
			id, 
			referralDate,
			"Clinic",
			"Doctor",
			ReferralStatus.Scheduled,
			ReferralPriority.Low);
		var rmpr = new ReferralMissingProfessionalRule();
		var result = rmpr.Evaluate(referral);

		Assert.Null(result);
	}

}