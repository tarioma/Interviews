using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.WorkflowTemplates.Queries;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.WorkflowTemplates.Handlers;

public class GetWorkflowTemplateByIdHandler
{
    private readonly ITenantRepository _tenant;

    public GetWorkflowTemplateByIdHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Employee Handle(GetWorkflowTemplateByIdQuery query)
    {
        Guard.Against.Null(query);

        return _tenant.Employees.GetById(query.WorkflowTemplateId);
    }
}