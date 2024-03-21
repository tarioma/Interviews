using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.CreateRequest;

public class CreateRequestHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Guid Handle(CreateRequestCommand command)
    {
        Guard.Against.Null(command);

        var request = Request.Create(command.Document, command.Workflow, command.EmployeeId);
        Tenant.Requests.Create(request);
        Tenant.Commit();

        return request.Id;
    }
}