using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.CreateEmployee;

public class CreateEmployeeHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Guid Handle(CreateEmployeeCommand command)
    {
        Guard.Against.Null(command);

        var employee = Employee.Create(command.Name, command.EmailAddress, command.RoleId);
        Tenant.Employees.Create(employee);
        Tenant.Commit();

        return employee.Id;
    }
}