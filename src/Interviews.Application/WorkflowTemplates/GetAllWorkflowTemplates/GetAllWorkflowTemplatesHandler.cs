using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.GetAllWorkflowTemplates;

public class GetAllWorkflowTemplatesHandler : Handler<GetAllWorkflowTemplatesQuery, IEnumerable<WorkflowTemplate>>
{
    public GetAllWorkflowTemplatesHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override IEnumerable<WorkflowTemplate> Handle(GetAllWorkflowTemplatesQuery command)
    {
        Guard.Against.Null(command);

        return TenantFactory.WorkflowTemplates.GetAll();
    }
}