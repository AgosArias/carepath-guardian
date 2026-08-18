using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.Application.Referrals.GetReferralById;
public class GetReferralByIdHandler
{
	private readonly IReferralRepository _referralRepository;

	public GetReferralByIdHandler(IReferralRepository referralRepository)
	{
		_referralRepository = referralRepository;
	}
	
	public async Task<Referral?> Handle(GetReferralByIdQuery query)
	{
		return await _referralRepository.GetByIdAsync(query.Id);
	}
}