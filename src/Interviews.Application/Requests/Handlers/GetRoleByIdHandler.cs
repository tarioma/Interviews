using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Requests.Queries;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Requests.Handlers;

public class GetRoleByIdHandler
{
    private readonly ITenantRepository _tenant;

    public GetRoleByIdHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Role Handle(GetRoleByIdQuery request)
    {
        Guard.Against.Null(request);

        return _tenant.Roles.GetById(request.RoleId);
    }
}