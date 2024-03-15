using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Roles.Commands;

namespace Interviews.Application.Roles.Handlers;

public class DeleteRoleHandler
{
    private readonly ITenantRepository _tenant;

    public DeleteRoleHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public void Handle(DeleteRoleCommand command)
    {
        Guard.Against.Null(command);

        var role = _tenant.Roles.GetById(command.RoleId);
        Guard.Against.Null(role);

        _tenant.Roles.Delete(role);
        _tenant.Commit();
    }
}