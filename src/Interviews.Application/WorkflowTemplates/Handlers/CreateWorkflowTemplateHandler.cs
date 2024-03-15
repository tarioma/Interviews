using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.WorkflowTemplates.Commands;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.Handlers;

public class CreateWorkflowTemplateHandler
{
    private readonly ITenantRepository _tenant;

    public CreateWorkflowTemplateHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Guid Handle(CreateWorkflowTemplateCommand command)
    {
        Guard.Against.Null(command);

        var workflowTemplate = WorkflowTemplate.Create(command.Name, command.Steps);
        _tenant.WorkflowTemplates.Create(workflowTemplate);
        _tenant.Commit();

        return workflowTemplate.Id;
    }
}