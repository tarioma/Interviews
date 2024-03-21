using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.CreateRole;

public class CreateRoleHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Guid Handle(CreateRoleCommand command)
    {
        Guard.Against.Null(command);

        var role = Role.Create(command.Name);
        Tenant.Roles.Create(role);
        Tenant.Commit();

        return role.Id;
    }
}