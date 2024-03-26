using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.GetAllRoles;

public class GetAllRolesHandler : Handler<GetAllRolesQuery, IEnumerable<Role>>
{
    public GetAllRolesHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override IEnumerable<Role> Handle(GetAllRolesQuery command)
    {
        Guard.Against.Null(command);

        return TenantFactory.Roles.GetAll();
    }
}