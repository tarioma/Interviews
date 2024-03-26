using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.CreateWorkflowTemplate;

public class CreateWorkflowTemplateHandler : Handler<CreateWorkflowTemplateCommand, Guid>
{
    public CreateWorkflowTemplateHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Guid Handle(CreateWorkflowTemplateCommand command)
    {
        Guard.Against.Null(command);

        var workflowTemplate = WorkflowTemplate.Create(command.Name, command.Steps);
        TenantFactory.WorkflowTemplates.Create(workflowTemplate);
        TenantFactory.Commit();

        return workflowTemplate.Id;
    }
}