using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.GetWorkflowTemplateId;

public class GetWorkflowTemplateByIdHandler : Handler<GetWorkflowTemplateByIdQuery, WorkflowTemplate>
{
    public GetWorkflowTemplateByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override WorkflowTemplate Handle(GetWorkflowTemplateByIdQuery command)
    {
        Guard.Against.Null(command);

        var workflowTemplate = TenantFactory.WorkflowTemplates.TryGetById(command.WorkflowTemplateId);

        if (workflowTemplate is null)
        {
            throw new Exception($"Нет {nameof(WorkflowTemplate)} с таким id.");
        }

        return workflowTemplate;
    }
}