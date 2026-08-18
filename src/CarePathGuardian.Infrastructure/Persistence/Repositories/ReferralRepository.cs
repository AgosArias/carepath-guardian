using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.Referrals;

namespace CarePathGuardian.Infrastructure.Persistence.Repositories;
public class ReferralRepository : IReferralRepository
{
	private readonly CarePathGuardianDbContext _dbContext;

	public ReferralRepository(CarePathGuardianDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddAsync(Referral referral)
	{
		await _dbContext.Referrals.AddAsync(referral);
		await _dbContext.SaveChangesAsync();
	}
	public async Task<Referral?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Referrals.FindAsync(id);
	}
}