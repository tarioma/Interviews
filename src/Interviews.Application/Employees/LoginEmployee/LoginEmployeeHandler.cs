using Ardalis.GuardClauses;
using Interviews.Application.Employees.RegisterEmployee;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.LoginEmployee;

public class RegisterEmployeeHandler : Handler<RegisterEmployeeCommand, Guid>
{
    public RegisterEmployeeHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Guid Handle(RegisterEmployeeCommand command)
    {
        var employee = TenantFactory.Employees.TryGetByEmail(command.EmailAddress)
                       ?? throw new Exception($"Нет {nameof(Employee)} с таким email-адресом.");

        if (employee.AuthData.PasswordHash != command.AuthData.PasswordHash)
        {
            throw new Exception("Пароль неверный.");
        }

        return employee.Id;
    }
}