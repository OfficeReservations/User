using User.Domain.Common.Domain;
using User.Domain.ValueObject;

namespace User.Domain;

public class Teacher : Entity<Guid>
{
    public string PersonalNumber { get; private set; }
    public string Password { get; private set; }
    public FullName FullName { get; private set; }
    
    public Teacher(Guid id, string personalNumber, FullName fullName)
    {
        Id = id;
        PersonalNumber = personalNumber ?? throw new ArgumentNullException(nameof(personalNumber));
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
    }
    
    protected Teacher() { }


    public void SetPassword(string password)
    {
        Password = BCrypt.Net.BCrypt.HashPassword(password);
    }


    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }


    public void ChangeFullName(FullName newFullName)
    {
        FullName = newFullName ?? throw new ArgumentNullException(nameof(newFullName));
    }
}