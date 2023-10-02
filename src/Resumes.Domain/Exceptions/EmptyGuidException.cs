namespace Resumes.Domain.Exceptions;

public class EmptyGuidException : Exception
{
    public EmptyGuidException()
    {
    }
    
    public EmptyGuidException(string? message)
        : base(message)
    {
    }

    public static void ThrowIfEmpty(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new EmptyGuidException();
        }
    }
}