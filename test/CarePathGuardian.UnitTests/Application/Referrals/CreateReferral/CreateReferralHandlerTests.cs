using CarePathGuardian.Application.Referrals.CreateReferral;
using CarePathGuardian.Domain.Referrals;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Referrals.CreateReferral;
public class CreateReferralHandlerTests
{
	[Fact]
	public async Task Handle_ShouldAddReferral()
	{
		Guid id = Guid.NewGuid();

		var repository = new FakeReferralRepository();
		var handler = new CreateReferralHandle(repository);

		var command = new CreateReferralCommand(id, 
		new DateOnly(2026, 5, 20),
		"",
		"",
		ReferralStatus.Pending,
		ReferralPriority.High
		);

		await handler.Handle(command);
		Assert.NotNull(repository.AddedReferral);
		Assert.Equal(id, command.patientId);
	}
}
