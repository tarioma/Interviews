using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.Roles.UpdateRole;

public class UpdateRoleHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(UpdateRoleCommand command)
    {
        Guard.Against.Null(command);

        Tenant.Roles.Update(command.Role);
        Tenant.Commit();
    }
}