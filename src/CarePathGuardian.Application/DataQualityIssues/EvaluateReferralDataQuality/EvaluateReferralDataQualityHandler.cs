using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Domain.DataQualityRules;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.Domain.Appointments;

namespace CarePathGuardian.Application.DataQualityIssues.EvaluateReferralDataQuality;

public class EvaluateReferralDataQualityHandler
{
	private readonly IDataQualityIssueRepository _dataQualityIssueRepository;

	public EvaluateReferralDataQualityHandler(
		IDataQualityIssueRepository dataQualityIssueRepository
	)
	{
		_dataQualityIssueRepository = dataQualityIssueRepository;
	}
	public async Task Handle(
		Referral referral, 
		IEnumerable<Appointment> appointments)
	{
        IReferralDataQualityRule[] rules =
        {
			new ReferralPendingOver14DaysRule(),
            new CancelledAppointmentNotRescheduledRule(),
            new AppointmentBeforeReferralRule()
        };
		foreach( var rule in rules)
		{
			var issue = rule.Evaluate(referral,appointments);
			if(issue is not null)
			{
				await _dataQualityIssueRepository.AddAsync(issue);
			}
		}

	}
}