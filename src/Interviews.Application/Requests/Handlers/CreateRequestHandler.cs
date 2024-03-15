using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Requests.Commands;

namespace Interviews.Application.Requests.Handlers;

public class CreateRequestHandler
{
    private readonly ITenantRepository _tenant;

    public CreateRequestHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Guid Handle(CreateRequestCommand command)
    {
        Guard.Against.Null(command);

        var workflowTemplate = _tenant.WorkflowTemplates.GetById(command.WorkflowTemplateId);
        Guard.Against.Null(workflowTemplate);

        var employee = _tenant.Employees.GetById(command.EmployeeId);
        Guard.Against.Null(employee);

        var request = workflowTemplate.CreateRequest(employee, command.Document);
        _tenant.Requests.Create(request);
        _tenant.Commit();

        return request.Id;
    }
}