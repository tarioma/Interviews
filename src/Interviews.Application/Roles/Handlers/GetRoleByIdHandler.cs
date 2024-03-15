using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Roles.Queries;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.Handlers;

public class GetRequestByIdHandler
{
    private readonly ITenantRepository _tenant;

    public GetRequestByIdHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Role Handle(GetRequestByIdQuery query)
    {
        Guard.Against.Null(query);

        return _tenant.Roles.GetById(query.RoleId);
    }
}