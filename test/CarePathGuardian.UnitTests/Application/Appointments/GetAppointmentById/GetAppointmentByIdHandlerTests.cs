using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CarePathGuardian.Application.Appointments.CreateAppointment;
using CarePathGuardian.Application.Appointments.GetAppointmentById;
using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.UnitTests.Fakes;

namespace CarePathGuardian.UnitTests.Application.Appointments.GetAppointmentById;
public class GetAppointmentByIdHandlerTests
{
	[Fact]
	public async Task Handle_WhenAppointmentExists_ShouldReturnAppointment()
	{
		Guid id = Guid.NewGuid();
		var appointment = new Appointment(
			id,
			DateTime.UtcNow.AddDays(-15),
			AppointmentStatus.Completed,
			null
		);

		var repository = new FakeAppointmentRepository
		{
			AppointmentToReturn = appointment
		};

		var handler = new GetAppointmentByIdHandler(repository);
		var query = new GetAppointmentByIdQuery(appointment.Id);

		var result = await handler.Handle(query);
		
		Assert.NotNull(result);
		Assert.Equal(appointment.Id,result.Id);
		Assert.Equal(id, result.ReferralId);
	}

	[Fact]
	public async Task Handle_WhenAppointmentDoesNotExist_ShouldReturnNull()
	{
		var repository = new FakeAppointmentRepository
		{
			AppointmentToReturn = null
		};

		var handler = new GetAppointmentByIdHandler(repository);
		var query = new GetAppointmentByIdQuery(Guid.NewGuid());

		var result = await handler.Handle(query);
		Assert.Null(result);
	}
}