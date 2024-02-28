using AutoFixture;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Domain.Tests.Tools;

public class DocumentCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var dateOfBirth = DateOnly.FromDateTime(
            DateTime.UtcNow.AddYears(-Document.MaxNameLength));
        
        fixture.Customize<Document>(composer =>
            composer.With(doc => doc.DateOfBirth, dateOfBirth));
    }
}