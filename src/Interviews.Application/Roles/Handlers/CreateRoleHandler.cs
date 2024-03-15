using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Roles.Commands;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.Handlers;

public class CreateRoleHandler
{
    private readonly ITenantRepository _tenant;

    public CreateRoleHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Guid Handle(CreateRoleCommand command)
    {
        Guard.Against.Null(command);

        var role = Role.Create(command.Name);
        _tenant.Roles.Create(role);
        _tenant.Commit();

        return role.Id;
    }
}