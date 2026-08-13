using System;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.UnitTests.DataQualityRules;
public class ReferralMissingClinicRuleTest
{
	[Fact]
	public void Evaluate_WhenReferralHasNoClinic_ShouldReturnIssue()
	{
		Guid id = Guid.NewGuid();
		var dateOfBirth = new DateOnly(1995,5,20);

		var referral = new Referral(
			id, 
			dateOfBirth,
			"",
			"",
			ReferralStatus.Scheduled,
			ReferralPriority.Low);

		var rmcr = new ReferralMissingClinicRule();
		var result = rmcr.Evaluate(referral);

		Assert.NotNull(result);
		Assert.Equal("REFERRAL_MISSING_CLINIC", result.RuleCode);
		Assert.Equal(referral.Id, result.EntityId);
		Assert.Equal(IssueSeverity.Medium, result.Severity);

	}
	[Fact]
	public void Evaluate_WhenReferralHasClinic_ShouldReturnNull()
	{
		Guid id = Guid.NewGuid();
		var dateOfBirth = new DateOnly(1995,5,20);

		var referral = new Referral(
			id, 
			dateOfBirth,
			"Clinic",
			"",
			ReferralStatus.Scheduled,
			ReferralPriority.Low);

		var rmcr = new ReferralMissingClinicRule();
		var result = rmcr.Evaluate(referral);

		Assert.Null(result);
	}
}
