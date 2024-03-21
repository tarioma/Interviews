using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.WorkflowTemplates.DeleteWorkflowTemplate;

public class DeleteWorkflowTemplateHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(DeleteWorkflowTemplateCommand command)
    {
        Guard.Against.Null(command);

        Tenant.WorkflowTemplates.Delete(command.WorkflowTemplateId);
        Tenant.Commit();
    }
}