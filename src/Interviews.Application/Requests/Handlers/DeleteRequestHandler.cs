using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Requests.Commands;

namespace Interviews.Application.Requests.Handlers;

public class DeleteRequestHandler
{
    private readonly ITenantRepository _tenant;

    public DeleteRequestHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public void Handle(DeleteRequestCommand command)
    {
        Guard.Against.Null(command);

        var request = _tenant.Requests.GetById(command.RequestId);
        Guard.Against.Null(request);

        _tenant.Requests.Delete(request);
        _tenant.Commit();
    }
}