using CarePathGuardian.Application.Patients.GetPatientById;
using CarePathGuardian.Application.Referrals.GetReferralById;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Referrals.GetReferralById;
public class GetReferralByIdHandlerTests
{
	[Fact]
	public async Task Handle_WhenReferralExists_ShouldReturnReferral()
	{
		Guid id = Guid.NewGuid();

		var referral = new Referral(id, 
		new DateOnly(2026, 5, 20),
		"",
		"",
		ReferralStatus.Pending,
		ReferralPriority.High
		);

		var repository = new FakeReferralRepository
		{
			ReferralToReturn = referral
		};
		var handler = new GetReferralByIdHandler(repository);
		var query = new GetReferralByIdQuery(referral.Id);
		var result = await handler.Handle(query);
		Assert.NotNull(result);
		Assert.Equal(referral.Id, result.Id);
		Assert.Equal(id,result.PatientId);
	}
	
	[Fact]
	public async Task Handle_WhenReferralDoesNotExist_ShouldReturnNull()
	{
		var repository = new FakeReferralRepository
		{
			ReferralToReturn = null
		};
		var handler = new GetReferralByIdHandler(repository);
		var query = new GetReferralByIdQuery(Guid.NewGuid());
		var result = await handler.Handle(query);
		Assert.Null(result);
	}
}