using Ardalis.GuardClauses;
using Interviews.Application.Employees.Commands;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities;

namespace Interviews.Application.Employees.Handlers;

public class UpdateEmployeeHandler
{
    private readonly ITenantRepository _tenant;

    public UpdateEmployeeHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public void Handle(UpdateEmployeeNameCommand command)
    {
        Guard.Against.Null(command);

        var employee = _tenant.Employees.GetById(command.EmployeeId);
        Guard.Against.Null(employee);

        employee.SetName(command.Name);
        _tenant.Employees.Update(employee);
        _tenant.Commit();
    }

    public void Handle(UpdateEmployeeEmailAddressCommand command)
    {
        Guard.Against.Null(command);

        var employee = _tenant.Employees.GetById(command.EmployeeId);
        Guard.Against.Null(employee);

        var emailAddress = new EmailAddress(command.EmailAddress);
        employee.SetEmailAddress(emailAddress);
        _tenant.Employees.Update(employee);
        _tenant.Commit();
    }
}