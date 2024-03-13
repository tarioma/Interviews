using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Requests;

public class Document
{
    internal const int MaxNameLength = 100;
    internal const int MinAcceptableAge = 18;

    public Document(string name, DateOnly dateOfBirth, EmailAddress emailAddress)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.Default(dateOfBirth);
        Guard.Against.Null(emailAddress);

        if (!IsAgeAcceptable(dateOfBirth))
        {
            throw new ArgumentException($"Минимальный допустимый возраст {MinAcceptableAge} лет.",
                nameof(dateOfBirth));
        }

        Name = name;
        DateOfBirth = dateOfBirth;
        EmailAddress = emailAddress;
    }

    public string Name { get; }
    public DateOnly DateOfBirth { get; }
    public EmailAddress EmailAddress { get; }

    private static bool IsAgeAcceptable(DateOnly dateOfBirth)
    {
        var currentYear = DateTime.UtcNow.Year;
        var age = currentYear - dateOfBirth.Year;

        return age >= MinAcceptableAge;
    }
}