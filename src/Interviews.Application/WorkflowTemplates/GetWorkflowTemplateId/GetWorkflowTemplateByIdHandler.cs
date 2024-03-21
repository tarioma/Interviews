using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.GetWorkflowTemplateId;

public class GetWorkflowTemplateByIdHandler(ITenantRepository tenant) : Handler(tenant)
{
    public WorkflowTemplate Handle(GetWorkflowTemplateByIdQuery command)
    {
        Guard.Against.Null(command);

        return Tenant.WorkflowTemplates.GetById(command.RoleId);
    }
}