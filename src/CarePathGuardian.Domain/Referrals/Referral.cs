using System;

namespace CarePathGuardian.Domain.Referrals;

public class Referral
{
	public Guid Id { get; private set; }
	public Guid PatientId {get; private set; }
	public DateOnly ReferralDate {get; private set; }
	public string? AssignedClinic { get; private set; }
	public string? AssignedProfessional { get; private set; }
	public ReferralStatus Status {get; private set; }
	public ReferralPriority Priority {get; private set; }
	public DateTime CreatedAtUtc {get; private set; }
	public DateTime UpdatedAtUtc {get; private set; }

	private Referral()
	{}

	public Referral(
		Guid patientId, 
		DateOnly referralDate, 
		string? assignedClinic, 
		string? assignedProfessional,
		ReferralStatus status,
		ReferralPriority priority
		)
	{
		if(patientId == Guid.Empty){throw new ArgumentException("Patient id is required", nameof(patientId));}
		if(referralDate > DateOnly.FromDateTime(DateTime.UtcNow)){throw new ArgumentException("Referral date cannot be in the future.", nameof(referralDate));}

		Id = Guid.NewGuid();
		PatientId = patientId;
		ReferralDate = referralDate;
		AssignedClinic = string.IsNullOrWhiteSpace(assignedClinic)?null:assignedClinic.Trim();
		AssignedProfessional = string.IsNullOrWhiteSpace(assignedProfessional)?null:assignedProfessional.Trim();
		Status = status;
		Priority = priority;
		CreatedAtUtc = DateTime.UtcNow;
		UpdatedAtUtc = DateTime.UtcNow;
	}
}