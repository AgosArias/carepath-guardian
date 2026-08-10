using CarePathGuardian.Domain.Referrals;


namespace CarePathGuardian.UnitTests.Referrals;
public class ReferralTests
{
	[Fact]
	public void Constructor_WhenDataIsValid_ShouldCreateReferral()
	{
		var dateRef = new DateOnly(2023,10,10);
		Guid patientId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		var referrals = new Referral(patientId,dateRef,"Clinica A","Doctora A",ReferralStatus.Closed,ReferralPriority.Low);

		Assert.NotEqual(Guid.Empty, referrals.Id);
		Assert.Equal(patientId, referrals.PatientId);
		Assert.Equal(dateRef, referrals.ReferralDate);
		Assert.Equal("Clinica A", referrals.AssignedClinic);
		Assert.Equal("Doctora A", referrals.AssignedProfessional);
		Assert.Equal(ReferralStatus.Closed, referrals.Status);
		Assert.Equal(ReferralPriority.Low, referrals.Priority);
		Assert.True(referrals.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(referrals.UpdatedAtUtc <= DateTime.UtcNow);
	}
	[Fact]
	public void Constructor_WhenPatientIdIsEmpty_ShouldThrowArgumentException()
	{
		var dateRef = new DateOnly(2023,10,10);
		Guid patientId = Guid.Empty;
		Action action = () => new Referral(patientId,dateRef,"Clinica A","Doctora A",ReferralStatus.Closed,ReferralPriority.Low);

		var exception = Assert.Throws<ArgumentException>(action);
		Assert.Equal("patientId", exception.ParamName);
	}
	[Fact]
	public void Constructor_WhenReferralDateIsInFuture_ShouldThrowArgumentException()
	{
		var dateRef = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
		Guid patientId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		Action action = () => new Referral(patientId,dateRef,"Clinica A","Doctora A",ReferralStatus.Closed,ReferralPriority.Low);

		var exception = Assert.Throws<ArgumentException>(action);
		Assert.Equal("referralDate", exception.ParamName);
	}
	[Fact]
	public void Constructor_WhenAssignedClinicIsEmpty_ShouldSetClinicToNull()
	{
		var dateRef = new DateOnly(2023,10,10);
		Guid patientId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		var referrals = new Referral(patientId,dateRef,"","Doctora A",ReferralStatus.Closed,ReferralPriority.Low);

		Assert.NotEqual(Guid.Empty, referrals.Id);
		Assert.Equal(patientId, referrals.PatientId);
		Assert.Null(referrals.AssignedClinic);
		Assert.Equal(dateRef, referrals.ReferralDate);
		Assert.Equal("Doctora A", referrals.AssignedProfessional);
		Assert.Equal(ReferralStatus.Closed, referrals.Status);
		Assert.Equal(ReferralPriority.Low, referrals.Priority);
		Assert.True(referrals.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(referrals.UpdatedAtUtc <= DateTime.UtcNow);
	}
	[Fact]
	public void Constructor_WhenAssignedProfessionalIsEmpty_ShouldSetProfessionalToNull()
	{
		var dateRef = new DateOnly(2023,10,10);
		Guid patientId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		var referrals = new Referral(patientId,dateRef,"Clinica A","",ReferralStatus.Closed,ReferralPriority.Low);

		Assert.NotEqual(Guid.Empty, referrals.Id);
		Assert.Equal(patientId, referrals.PatientId);
		Assert.Equal(dateRef, referrals.ReferralDate);
		Assert.Equal("Clinica A", referrals.AssignedClinic);
		Assert.Null(referrals.AssignedProfessional);
		Assert.Equal(ReferralStatus.Closed, referrals.Status);
		Assert.Equal(ReferralPriority.Low, referrals.Priority);
		Assert.True(referrals.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(referrals.UpdatedAtUtc <= DateTime.UtcNow);
	}
	[Fact]
	public void Constructor_WhenAssignedClinicHasSpaces_ShouldTrimClinic()
	{
		var dateRef = new DateOnly(2023,10,10);
		Guid patientId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		var referrals = new Referral(patientId,dateRef," Clinica A ","Doctora A",ReferralStatus.Closed,ReferralPriority.Low);

		Assert.NotEqual(Guid.Empty, referrals.Id);
		Assert.Equal(patientId, referrals.PatientId);
		Assert.Equal(dateRef, referrals.ReferralDate);
		Assert.Equal("Clinica A", referrals.AssignedClinic);
		Assert.Equal("Doctora A", referrals.AssignedProfessional);
		Assert.Equal(ReferralStatus.Closed, referrals.Status);
		Assert.Equal(ReferralPriority.Low, referrals.Priority);
		Assert.True(referrals.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(referrals.UpdatedAtUtc <= DateTime.UtcNow);
	}
	[Fact]
	public void Constructor_WhenAssignedProfessionalHasSpaces_ShouldTrimProfessional()
	{
		var dateRef = new DateOnly(2023,10,10);
		Guid patientId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
		var referrals = new Referral(patientId,dateRef,"Clinica A"," Doctora A ",ReferralStatus.Closed,ReferralPriority.Low);

		Assert.NotEqual(Guid.Empty, referrals.Id);
		Assert.Equal(patientId, referrals.PatientId);
		Assert.Equal(dateRef, referrals.ReferralDate);
		Assert.Equal("Clinica A", referrals.AssignedClinic);
		Assert.Equal("Doctora A", referrals.AssignedProfessional);
		Assert.Equal(ReferralStatus.Closed, referrals.Status);
		Assert.Equal(ReferralPriority.Low, referrals.Priority);
		Assert.True(referrals.CreatedAtUtc <= DateTime.UtcNow);
		Assert.True(referrals.UpdatedAtUtc <= DateTime.UtcNow);
	}
}