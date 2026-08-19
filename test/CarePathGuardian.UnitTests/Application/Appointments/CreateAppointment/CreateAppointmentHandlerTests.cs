using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarePathGuardian.Application.Appointments.CreateAppointment;
using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Appointments.CreateAppointment;
public class CreateAppointmentHandlerTests
{
	[Fact]
	public async Task Handle_ShouldAddAppointment()
	{
		var repository = new FakeAppointmentRepository();
		var handler = new CreateAppointmentHandler(repository);

		Guid id = Guid.NewGuid();
		var command = new CreateAppointmentCommand(
			id,
			DateTime.UtcNow.AddDays(-15),
			AppointmentStatus.Completed,
			null
		);

		await handler.Handle(command);
		Assert.NotNull(repository.AddedAppointment);
		Assert.Equal(id, repository.AddedAppointment.ReferralId);
	}
}