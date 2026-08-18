using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Application.Abstractions.Persistence;

namespace CarePathGuardian.Application.Referrals.CreateReferral;
public class CreateReferralHandle
{
	private readonly IReferralRepository _referralRepository;

	public CreateReferralHandle(IReferralRepository referralRepository)
	{
		_referralRepository = referralRepository;
	}

	public async Task<Referral> Handle(CreateReferralCommand command)
	{
		var referral = new Referral(
			command.patientId,
			command.referralDate,
			command.assignedClinic,
			command.assignedProfessional,
			command.status,
			command.priority);

		await _referralRepository.AddAsync(referral);
		return referral;
		
	}
}