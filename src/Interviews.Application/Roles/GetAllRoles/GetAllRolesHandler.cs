using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.GetAllRoles;

public class GetAllRolesHandler(ITenantRepository tenant) : Handler(tenant)
{
    public IEnumerable<Role> Handle(GetAllRolesQuery command)
    {
        Guard.Against.Null(command);

        return Tenant.Roles.GetAll();
    }
}