using System.ComponentModel.DataAnnotations;
using Resumes.Domain.Entities;

namespace Resumes.Domain.InterviewRequest;

public class Document
{
    // Максимальный возраст для валидации дня рождения
    private const int MaxAgeForValidation = 100;
    
    public string Name { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public DateOnly Birthday { get; private set; }

    public Document(string name, EmailAddress emailAddress, DateOnly birthday)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(emailAddress);

        if (!IsBirthdayValid(birthday))
        {
            throw new ValidationException("День рождения невалиден");
        }

        Name = name;
        EmailAddress = emailAddress;
        Birthday = birthday;
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName);

        Name = newName;
    }
    
    public void UpdateEmailAddress(EmailAddress newEmailAddress)
    {
        ArgumentNullException.ThrowIfNull(newEmailAddress);

        EmailAddress = newEmailAddress;
    }
    
    public void UpdateBirthday(DateOnly newBirthday)
    {
        if (!IsBirthdayValid(newBirthday))
        {
            throw new ValidationException("День рождения невалиден");
        }

        Birthday = newBirthday;
    }

    private static bool IsBirthdayValid(DateOnly birthday)
    {
        var currentYear = DateTime.Today.Year;
        var minYear = currentYear - MaxAgeForValidation;
        
        return birthday.Year > minYear &&
               birthday.Year < currentYear;
    }
}