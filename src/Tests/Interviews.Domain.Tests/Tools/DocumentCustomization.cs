using AutoFixture;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Domain.Tests.Tools;

public class DocumentCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize(new EmailAddressCustomization());

        var name = fixture.GenerateString(Employee.MaxNameLength);
        var dateOfBirth = DateOnly.FromDateTime(
            DateTime.UtcNow.AddYears(-Document.MaxNameLength));
        var emailAddress = fixture.Create<EmailAddress>();

        fixture.Customize<Document>(composer =>
            composer.FromFactory(() =>
                new Document(name, dateOfBirth, emailAddress)));
    }
}