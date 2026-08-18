using CarePathGuardian.Domain.Referrals;

namespace CarePathGuardian.Application.Abstractions.Persistence;

public interface IReferralRepository
{
	Task AddAsync(Referral referral);
	Task<Referral?> GetByIdAsync(Guid id);
}