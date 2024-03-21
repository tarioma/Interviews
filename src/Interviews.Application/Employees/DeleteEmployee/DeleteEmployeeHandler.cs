using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.Employees.DeleteEmployee;

public class DeleteEmployeeHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(DeleteEmployeeCommand command)
    {
        Guard.Against.Null(command);

        Tenant.Employees.Delete(command.EmployeeId);
        Tenant.Commit();
    }
}