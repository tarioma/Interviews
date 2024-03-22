using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.CreateRequest;

public class CreateRequestHandler : Handler<CreateRequestCommand, Guid>
{
    public CreateRequestHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Guid Handle(CreateRequestCommand command)
    {
        var request = Request.Create(command.Document, command.Workflow, command.EmployeeId);
        TenantFactory.Requests.Create(request);
        TenantFactory.Commit();

        return request.Id;
    }
}