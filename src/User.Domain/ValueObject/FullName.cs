namespace User.Domain.ValueObject;

public class FullName : Common.Domain.ValueObject
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }

    public FullName(string firstName, string lastName, string middleName)
    {
        
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}