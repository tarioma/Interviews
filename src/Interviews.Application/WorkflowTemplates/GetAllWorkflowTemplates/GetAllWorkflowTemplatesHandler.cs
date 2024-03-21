using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Application.Roles.GetAllRoles;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.GetAllWorkflowTemplates;

public class GetAllWorkflowTemplatesHandler(ITenantRepository tenant) : Handler(tenant)
{
    public IEnumerable<WorkflowTemplate> Handle(GetAllWorkflowTemplatesQuery command)
    {
        Guard.Against.Null(command);

        return Tenant.WorkflowTemplates.GetAll();
    }
}