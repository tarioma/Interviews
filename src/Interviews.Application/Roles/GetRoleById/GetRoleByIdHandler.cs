using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.GetRoleById;

public class GetRoleByIdHandler : Handler<GetRoleByIdQuery, Role>
{
    public GetRoleByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Role Handle(GetRoleByIdQuery command) =>
        TenantFactory.Roles.TryGetById(command.RoleId)
        ?? throw new Exception($"Нет {nameof(Role)} с таким id.");
}