using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.GetAllRequests;

public class GetAllRequestsHandler : Handler<GetAllRequestsQuery, IEnumerable<Request>>
{
    public GetAllRequestsHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override IEnumerable<Request> Handle(GetAllRequestsQuery command) =>
        TenantFactory.Requests.GetAll();
}