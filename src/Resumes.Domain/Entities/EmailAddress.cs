using System.ComponentModel.DataAnnotations;

namespace Resumes.Domain.Entities;

public class EmailAddress
{
    public string Value { get; private set; }

    public EmailAddress(string value)
    {
        if (!value.Contains('@'))
        {
            throw new ValidationException("Email-адрес невалиден");
        }

        Value = value;
    }
}