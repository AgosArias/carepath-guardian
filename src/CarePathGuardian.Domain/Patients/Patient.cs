using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace CarePathGuardian.Domain.Patients;

public class Patient
{
	public Guid Id { get; private set; }
	public string ExternalReference { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public DateOnly DateOfBirth { get; private set; }
	public string? Email { get; private set; }
	public string? PhoneNumber { get; private set; }
	public DateTime CreatedAtUtc { get; private set; }
	public DateTime UpdatedAtUtc { get; private set; }

	private Patient()
	{
		ExternalReference = string.Empty;
		FirstName = string.Empty;
		LastName = string.Empty;
	}

	public Patient(
		string externalReference, 
		string firstName, 
		string lastName, 
		DateOnly dateBirth,
		string? email,
		string? phone
	)
	{
		if(string.IsNullOrWhiteSpace(externalReference)) { throw new ArgumentException("External reference is required.", nameof(externalReference));}
		if(string.IsNullOrWhiteSpace(firstName)) { throw new ArgumentException("First name is required.", nameof(firstName));}
		if(string.IsNullOrWhiteSpace(lastName)) { throw new ArgumentException("Last Name is required.", nameof(lastName));}
		if(dateBirth > DateOnly.FromDateTime(DateTime.UtcNow)){throw new ArgumentException("Date of birth cannot be in the future.", nameof(dateBirth));}

		Id = Guid.NewGuid();
		ExternalReference = externalReference.Trim();
		FirstName = firstName.Trim();
		LastName = lastName.Trim();
		DateOfBirth = dateBirth;
		Email = string.IsNullOrWhiteSpace(email)? null : email.Trim();
		PhoneNumber = string.IsNullOrWhiteSpace(phone)? null : phone.Trim();
		CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
	}
}
