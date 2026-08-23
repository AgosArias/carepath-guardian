using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Application.DataQualityIssues.EvaluateReferralDataQuality;
using CarePathGuardian.Domain.Appointments;

namespace CarePathGuardian.Application.Referrals.CreateReferral;
public class CreateReferralHandle
{
	private readonly IReferralRepository _referralRepository;
	private readonly EvaluateReferralDataQualityHandler _qualityHandler;

	public CreateReferralHandle(IReferralRepository referralRepository, EvaluateReferralDataQualityHandler qualityHandler)
	{
		_referralRepository = referralRepository;
		_qualityHandler = qualityHandler;
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

		await _qualityHandler.Handle(referral,new List<Appointment>());

		return referral;
	}
}