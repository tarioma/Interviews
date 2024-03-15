using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Requests.Queries;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Requests.Handlers;

public class GetAllRolesHandler
{
    private readonly ITenantRepository _tenant;

    public GetAllRolesHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public IReadOnlySet<Role> Handle(GetAllRolesQuery request)
    {
        Guard.Against.Null(request);

        return _tenant.Roles.GetAll();
    }
}