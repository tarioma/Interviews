using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.Employees.UpdateEmployee;

public class UpdateEmployeeHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(UpdateEmployeeCommand command)
    {
        Guard.Against.Null(command);

        Tenant.Employees.Update(command.Employee);
        Tenant.Commit();
    }
}