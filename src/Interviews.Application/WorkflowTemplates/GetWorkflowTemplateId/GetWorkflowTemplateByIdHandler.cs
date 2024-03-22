using Interviews.Application.Repositories;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.GetWorkflowTemplateId;

public class GetWorkflowTemplateByIdHandler : Handler<GetWorkflowTemplateByIdQuery, WorkflowTemplate>
{
    public GetWorkflowTemplateByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override WorkflowTemplate Handle(GetWorkflowTemplateByIdQuery command) =>
        TenantFactory.WorkflowTemplates.TryGetById(command.WorkflowTemplateId)
        ?? throw new Exception($"Нет {nameof(WorkflowTemplate)} с таким id.");
}