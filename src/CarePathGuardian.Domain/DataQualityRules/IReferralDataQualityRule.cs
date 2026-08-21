using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Domain.DataQualityRules;
public interface IReferralDataQualityRule
{
	DataQualityIssue? Evaluate( Referral referral, IEnumerable<Appointment> appointments);
}