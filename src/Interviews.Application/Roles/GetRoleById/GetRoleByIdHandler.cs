using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.GetRoleById;

public class GetRoleByIdHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Role Handle(GetRoleByIdQuery command)
    {
        Guard.Against.Null(command);

        return Tenant.Roles.GetById(command.RoleId);
    }
}