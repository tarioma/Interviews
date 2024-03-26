using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.GetRoleById;

public class GetRoleByIdHandler : Handler<GetRoleByIdQuery, Role>
{
    public GetRoleByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Role Handle(GetRoleByIdQuery command)
    {
        Guard.Against.Null(command);

        var role = TenantFactory.Roles.TryGetById(command.RoleId);

        if (role is null)
        {
            throw new Exception($"Нет {nameof(Role)} с таким id.");
        }

        return role;
    }
}