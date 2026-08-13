using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Referrals;

namespace CarePathGuardian.Domain.DataQualityRules;

public class ReferralMissingClinicRule
{
	public DataQualityIssue? Evaluate(Referral referral)
	{
		if(string.IsNullOrWhiteSpace(referral.AssignedClinic))
		{
			return new DataQualityIssue(
				"Referral",
				referral.Id,
				"REFERRAL_MISSING_CLINIC",
				"Referral has no Assigned Clinic",
				IssueSeverity.Medium);
		}
		return null;
	}

}
