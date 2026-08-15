using CarePathGuardian.Domain.Appointments;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Domain.Patients;
using CarePathGuardian.Domain.Referrals;
using Microsoft.EntityFrameworkCore;

namespace CarePathGuardian.Infrastructure.Persistence;

public class CarePathGuardianDbContext : DbContext
{
	public CarePathGuardianDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Appointment> Appointments => Set<Appointment>();
	public DbSet<Referral> Referrals => Set<Referral>();
	public DbSet<Patient> Patients => Set<Patient>();
	public DbSet<DataQualityIssue> DataQualityIssues => Set<DataQualityIssue>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Patient>(p =>
		{
			p.ToTable("Patients");
	
			p.HasKey(x => x.Id);
	
			p.Property(x => x.ExternalReference)
				.IsRequired()
				.HasMaxLength(100);
	
			p.Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(100);
	
			p.Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(100);
	
			p.Property(x => x.DateOfBirth)
				.IsRequired();
	
			p.Property(x => x.Email)
				.HasMaxLength(255);
	
			p.Property(x => x.PhoneNumber)
				.HasMaxLength(50);
	
			p.Property(x => x.CreatedAtUtc)
				.IsRequired();
	
			p.Property(x => x.UpdatedAtUtc)
				.IsRequired();
		});
	
		modelBuilder.Entity<Referral>(r =>
		{
			r.ToTable("Referrals");
	
			r.HasKey(x => x.Id);
	
			r.Property(x => x.PatientId)
				.IsRequired();
	
			r.Property(x => x.ReferralDate)
				.IsRequired();
	
			r.Property(x => x.AssignedClinic)
				.HasMaxLength(150);
	
			r.Property(x => x.AssignedProfessional)
				.HasMaxLength(150);
	
			r.Property(x => x.Status)
				.IsRequired();
	
			r.Property(x => x.Priority)
				.IsRequired();
	
			r.Property(x => x.CreatedAtUtc)
				.IsRequired();
	
			r.Property(x => x.UpdatedAtUtc)
				.IsRequired();
	
			r.HasOne<Patient>()
				.WithMany()
				.HasForeignKey(x => x.PatientId)
				.OnDelete(DeleteBehavior.Restrict);
		});
	
		modelBuilder.Entity<Appointment>(a =>
		{
			a.ToTable("Appointments");
	
			a.HasKey(x => x.Id);
	
			a.Property(x => x.ReferralId)
				.IsRequired();
	
			a.Property(x => x.ScheduledAtUtc)
				.IsRequired();
	
			a.Property(x => x.Status)
				.IsRequired();
	
			a.Property(x => x.CancellationReason)
				.HasMaxLength(500);
	
			a.Property(x => x.CreatedAtUtc)
				.IsRequired();
	
			a.Property(x => x.UpdatedAtUtc)
				.IsRequired();
	
			a.HasOne<Referral>()
				.WithMany()
				.HasForeignKey(x => x.ReferralId)
				.OnDelete(DeleteBehavior.Restrict);
		});
	
		modelBuilder.Entity<DataQualityIssue>(d =>
		{
			d.ToTable("DataQualityIssues");
	
			d.HasKey(x => x.Id);
	
			d.Property(x => x.EntityType)
				.IsRequired()
				.HasMaxLength(100);
	
			d.Property(x => x.EntityId)
				.IsRequired();
	
			d.Property(x => x.RuleCode)
				.IsRequired()
				.HasMaxLength(150);
	
			d.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(1000);
	
			d.Property(x => x.Severity)
				.IsRequired();
	
			d.Property(x => x.Status)
				.IsRequired();
	
			d.Property(x => x.DetectedAtUtc)
				.IsRequired();
	
			d.Property(x => x.ResolvedAtUtc);
	
			d.Property(x => x.ResolutionNotes)
				.HasMaxLength(1000);
		});
	}

}

