using Ardalis.GuardClauses;
using Interviews.Application.Employees.Commands;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.Handlers;

public class CreateEmployeeHandler
{
    private readonly ITenantRepository _tenant;

    public CreateEmployeeHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Guid Handle(CreateEmployeeCommand command)
    {
        Guard.Against.Null(command);

        var emailAddress = new EmailAddress(command.EmailAddress);
        var employee = Employee.Create(command.Name, emailAddress, command.RoleId);
        _tenant.Employees.Create(employee);
        _tenant.Commit();

        return employee.Id;
    }
}