using System.Net.Mail;
using AutoFixture;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Domain.Tests.Tools;

public class RoleCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        //fixture.Customize<Role>(
        //    composer => composer.Create<>();
    }
}