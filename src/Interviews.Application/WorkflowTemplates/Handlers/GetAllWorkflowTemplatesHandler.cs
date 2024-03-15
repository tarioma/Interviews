using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.WorkflowTemplates.Queries;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.Handlers;

public class GetAllWorkflowTemplatesHandler
{
    private readonly ITenantRepository _tenant;

    public GetAllWorkflowTemplatesHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public IReadOnlySet<WorkflowTemplate> Handle(GetAllWorkflowTemplatesQuery query)
    {
        Guard.Against.Null(query);

        return _tenant.WorkflowTemplates.GetAll();
    }
}