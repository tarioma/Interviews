using System.Net.Mail;
using AutoFixture;
using Interviews.Domain.Entities;

namespace Interviews.Domain.Tests.Tools;

public class EmailAddressCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<EmailAddress>(composer =>
            composer.FromFactory<MailAddress>(a => new EmailAddress(a.Address)));
    }
}