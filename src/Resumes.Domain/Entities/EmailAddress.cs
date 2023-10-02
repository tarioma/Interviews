using System.ComponentModel.DataAnnotations;

namespace Resumes.Domain.Entities;

public class EmailAddress
{
    public string Value { get; private set; }

    public EmailAddress(string value)
    {
        if (!IsEmailValid(value))
        {
            throw new ValidationException("Email-адрес невалиден");
        }

        Value = value;
    }
    
    private static bool IsEmailValid(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}