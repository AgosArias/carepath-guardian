using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Domain.DataQualityRules
{
    public class ReferralMissingProfessionalRule
    {
        public DataQualityIssue? Evaluate(Referral referral)
		{
			if(string.IsNullOrWhiteSpace(referral.AssignedProfessional))
			{
				return new DataQualityIssue("Referral", 
				referral.Id,
				"REFERRAL_MISSING_PROFESSIONAL",
				"Referral has no assigned professional.",
				IssueSeverity.Medium);
			}
			return null;
		}
    }
}