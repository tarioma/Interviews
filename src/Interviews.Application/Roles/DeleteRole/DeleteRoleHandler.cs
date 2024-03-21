using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.Roles.DeleteRole;

public class DeleteRoleHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(DeleteRoleCommand command)
    {
        Guard.Against.Null(command);

        Tenant.Employees.Delete(command.RoleId);
        Tenant.Commit();
    }
}