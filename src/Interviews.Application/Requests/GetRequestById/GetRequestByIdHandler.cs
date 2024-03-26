using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.GetRequestById;

public class GetRequestByIdHandler : Handler<GetRequestByIdQuery, Request>
{
    public GetRequestByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Request Handle(GetRequestByIdQuery command)
    {
        Guard.Against.Null(command);

        var request = TenantFactory.Requests.TryGetById(command.RequestId);

        if (request is null)
        {
            throw new Exception($"{nameof(Request)} с таким id не найден.");
        }

        return request;
    }
}