using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.WorkflowTemplates.UpdateWorkflowTemplate;

public class UpdateWorkflowTemplateHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(UpdateWorkflowTemplateCommand command)
    {
        Guard.Against.Null(command);

        Tenant.WorkflowTemplates.Update(command.WorkflowTemplate);
        Tenant.Commit();
    }
}