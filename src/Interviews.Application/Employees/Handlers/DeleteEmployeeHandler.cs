using Ardalis.GuardClauses;
using Interviews.Application.Employees.Commands;
using Interviews.Application.Repositories;

namespace Interviews.Application.Employees.Handlers;

public class DeleteEmployeeHandler
{
    private readonly ITenantRepository _tenant;

    public DeleteEmployeeHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public void Handle(DeleteEmployeeCommand command)
    {
        Guard.Against.Null(command);

        var employee = _tenant.Employees.GetById(command.EmployeeId);
        Guard.Against.Null(employee);

        _tenant.Employees.Delete(employee);
        _tenant.Commit();
    }
}