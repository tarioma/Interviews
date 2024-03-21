using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.GetAllRequests;

public class GetAllRequestsHandler(ITenantRepository tenant) : Handler(tenant)
{
    public IEnumerable<Request> Handle(GetAllRequestsQuery command)
    {
        Guard.Against.Null(command);

        return Tenant.Requests.GetAll();
    }
}