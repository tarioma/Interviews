using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.GetRequestById;

public class GetRequestByIdHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Request Handle(GetRequestByIdQuery command)
    {
        Guard.Against.Null(command);

        return Tenant.Requests.GetById(command.RequestId);
    }
}