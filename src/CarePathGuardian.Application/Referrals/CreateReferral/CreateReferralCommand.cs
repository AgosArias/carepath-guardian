using CarePathGuardian.Domain.Referrals;

namespace CarePathGuardian.Application.Referrals.CreateReferral;
public sealed record CreateReferralCommand(
		Guid patientId, 
		DateOnly referralDate, 
		string? assignedClinic, 
		string? assignedProfessional,
		ReferralStatus status,
		ReferralPriority priority);