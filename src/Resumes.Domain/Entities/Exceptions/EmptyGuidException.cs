namespace Resumes.Domain.Entities.Exceptions;

public class EmptyGuidException : ArgumentException
{
    public EmptyGuidException()
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