using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.WorkflowTemplates.Commands;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.Handlers;

public class DeleteWorkflowTemplateHandler
{
    private readonly ITenantRepository _tenant;

    public DeleteWorkflowTemplateHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public void Handle(DeleteWorkflowTemplateCommand command)
    {
        Guard.Against.Null(command);

        var workflowTemplate = _tenant.WorkflowTemplates.GetById(command.WorkflowTemplateId);
        Guard.Against.Null(workflowTemplate);

        _tenant.WorkflowTemplates.Delete(workflowTemplate);
        _tenant.Commit();
    }
}