using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.CreateWorkflowTemplate;

public class CreateWorkflowTemplateHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Guid Handle(CreateWorkflowTemplateCommand command)
    {
        Guard.Against.Null(command);

        var workflowTemplate = WorkflowTemplate.Create(command.Name, command.Steps);
        Tenant.WorkflowTemplates.Create(workflowTemplate);
        Tenant.Commit();

        return workflowTemplate.Id;
    }
}