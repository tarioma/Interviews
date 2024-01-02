using System.ComponentModel.DataAnnotations;
using Resumes.Domain.Entities;

namespace Resumes.Domain.InterviewRequest;

public class Document
{
    private const int MaxAgeForValidation = 100;
    
    public string Name { get; private set; } = null!;
    public EmailAddress EmailAddress { get; private set; } = null!;
    public DateOnly Birthday { get; private set; }
    public int Experience { get; private set; }

    private Document(string name, EmailAddress emailAddress, DateOnly birthday, int experience)
    {
        SetName(name);
        SetEmailAddress(emailAddress);
        SetBirthday(birthday);
        SetExperience(experience);
    }

    public static Document Create(string name, EmailAddress emailAddress, DateOnly birthday, int experience)
    {
        return new Document(name, emailAddress, birthday, experience);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }
    
    public void SetEmailAddress(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        EmailAddress = emailAddress;
    }
    
    public void SetBirthday(DateOnly birthday)
    {
        if (!IsBirthdayValid(birthday))
        {
            throw new ValidationException("День рождения невалиден");
        }

        Birthday = birthday;
    }

    private static bool IsBirthdayValid(DateOnly birthday)
    {
        var currentUTCYear = DateTime.UtcNow.Year;
        var minYear = currentUTCYear - MaxAgeForValidation;
        
        return birthday.Year > minYear && birthday.Year < currentUTCYear;
    }

    public void SetExperience(int experience)
    {
        if (experience < 0)
        {
            throw new ArgumentException($"nameof({experience}) не может быть отрицательным");
        }
        
        Experience = experience;
    }
}