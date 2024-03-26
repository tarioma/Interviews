using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.CreateRole;

public class CreateRoleHandler : Handler<CreateRoleCommand, Guid>
{
    public CreateRoleHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Guid Handle(CreateRoleCommand command)
    {
        Guard.Against.Null(command);

        var role = Role.Create(command.Name);
        TenantFactory.Roles.Create(role);
        TenantFactory.Commit();

        return role.Id;
    }
}