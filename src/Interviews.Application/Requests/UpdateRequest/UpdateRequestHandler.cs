using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.Requests.UpdateRequest;

public class UpdateRequestHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(UpdateRequestCommand command)
    {
        Guard.Against.Null(command);

        Tenant.Requests.Update(command.Request);
        Tenant.Commit();
    }
}