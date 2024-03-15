using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Roles.Commands;

namespace Interviews.Application.Roles.Handlers;

public class UpdateRoleHandler
{
    private readonly ITenantRepository _tenant;

    public UpdateRoleHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public void Handle(UpdateRoleNameCommand command)
    {
        Guard.Against.Null(command);

        var role = _tenant.Roles.GetById(command.RoleId);
        Guard.Against.Null(role);

        role.SetName(command.Name);
        _tenant.Roles.Update(role);
        _tenant.Commit();
    }
}