namespace Interviews.Domain.Entities.Requests;

public class Document
{
    private const int MaxEmailLength = 500;
    
    public string Email { get; private init; }

    public Document(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        
        if (email.Length > MaxEmailLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxEmailLength} символов.", nameof(email));
        }

        Email = email;
    }
}