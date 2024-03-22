using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.GetAllRoles;

public class GetAllRolesHandler : Handler<GetAllRolesQuery, IEnumerable<Role>>
{
    public GetAllRolesHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override IEnumerable<Role> Handle(GetAllRolesQuery command) =>
        TenantFactory.Roles.GetAll();
}