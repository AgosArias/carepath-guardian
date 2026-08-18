using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.UnitTests.Fakes;
public class FakeReferralRepository : IReferralRepository
{
	public Referral? AddedReferral{get; set;}

	public Task AddAsync(Referral referral)
	{
		AddedReferral = referral;
		return Task.CompletedTask;
	}

	public Referral? ReferralToReturn {get;set;}

	public Task<Referral?> GetByIdAsync(Guid id)
	{
		return Task.FromResult(ReferralToReturn);
	}
}