using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Members;

public class Member : Entity
{
    public Member(string firstName, string lastName, string documentNumber, string email, bool isActive)
    {
        FirstName = firstName;
        LastName = lastName;
        DocumentNumber = documentNumber;
        Email = email;
        IsActive = isActive;
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string DocumentNumber { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    public void Update(string firstName, string lastName, string documentNumber, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        DocumentNumber = documentNumber;
        Email = email;
    }
}