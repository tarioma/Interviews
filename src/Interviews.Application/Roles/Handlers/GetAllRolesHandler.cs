using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Roles.Queries;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Roles.Handlers;

public class GetAllRequestsHandler
{
    private readonly ITenantRepository _tenant;

    public GetAllRequestsHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public IReadOnlySet<Role> Handle(GetAllRequestsQuery query)
    {
        Guard.Against.Null(query);

        return _tenant.Roles.GetAll();
    }
}