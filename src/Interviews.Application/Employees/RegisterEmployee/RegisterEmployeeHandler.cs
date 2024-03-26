using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.RegisterEmployee;

public class RegisterEmployeeHandler : Handler<RegisterEmployeeCommand, Guid>
{
    public RegisterEmployeeHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Guid Handle(RegisterEmployeeCommand command)
    {
        Guard.Against.Null(command);

        if (TenantFactory.Employees.TryGetByEmail(command.EmailAddress) is not null)
        {
            throw new Exception($"Нет {nameof(Employee)} с таким email-адресом.");
        }

        var employee = Employee.Create(command.Name, command.EmailAddress, command.RoleId, command.AuthData);
        TenantFactory.Employees.Create(employee);
        TenantFactory.Commit();

        return employee.Id;
    }
}