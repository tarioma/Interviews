/*using AutoFixture;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Domain.Tests.Tools;

public class RoleCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var name = fixture.GenerateString(Employee.MaxNameLength);
        
        fixture.Customize<Employee>(composer =>
            composer.With(r =>  r.Name = name));
        
        fixture.Customize<Employee>(composer =>
            composer.With(p => p.Name, new Employee()));
    }
}*/