using CarePathGuardian.Domain.Appointments;

namespace CarePathGuardian.UnitTests.Appointments;
public class AppointmentTests
{
	[Fact]
    public void Constructor_WhenDataIsValid_ShouldCreateAppointment()
	{
		Guid referralId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);
		var appointment = new Appointment(referralId, scheduledAtUtc, AppointmentStatus.Cancelled, "change date");

		Assert.NotEqual(Guid.Empty, appointment.Id);
		Assert.Equal(referralId, appointment.ReferralId);
		Assert.Equal(scheduledAtUtc, appointment.ScheduledAtUtc);
		Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
		Assert.Equal("change date", appointment.CancellationReason);
		Assert.True(appointment.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(appointment.UpdatedAtUtc <= DateTime.UtcNow);
	}
	[Fact]
	public void Constructor_WhenReferralIdIsEmpty_ShouldThrowArgumentException()
	{
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);
		Action action = () => new Appointment(Guid.Empty, scheduledAtUtc, AppointmentStatus.Cancelled, "change date");

		var exception = Assert.Throws<ArgumentException>(action);
		Assert.Equal("referralId", exception.ParamName);
	}
	[Fact]
	public void Constructor_WhenCancellationReasonIsEmpty_ShouldSetCancellationReasonToNull()
	{
		Guid referralId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);
		var appointment = new Appointment(referralId, scheduledAtUtc, AppointmentStatus.Cancelled,"");

		Assert.NotEqual(Guid.Empty, appointment.Id);
		Assert.Equal(referralId, appointment.ReferralId);
		Assert.Equal(scheduledAtUtc, appointment.ScheduledAtUtc);
		Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
		Assert.Null(appointment.CancellationReason);
		Assert.True(appointment.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(appointment.UpdatedAtUtc <= DateTime.UtcNow);
	}
	
	[Fact]
	public void Constructor_WhenCancellationReasonHasSpaces_ShouldTrimCancellationReason()
	{
		Guid referralId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		DateTime scheduledAtUtc = DateTime.UtcNow.AddDays(10);
		var appointment = new Appointment(referralId, scheduledAtUtc, AppointmentStatus.Cancelled, "   change date ");

		Assert.NotEqual(Guid.Empty, appointment.Id);
		Assert.Equal(referralId, appointment.ReferralId);
		Assert.Equal(scheduledAtUtc, appointment.ScheduledAtUtc);
		Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
		Assert.Equal("change date", appointment.CancellationReason);
		Assert.True(appointment.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(appointment.UpdatedAtUtc <= DateTime.UtcNow);
	}
}
