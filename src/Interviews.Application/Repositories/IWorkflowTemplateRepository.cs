using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.Repositories;

public interface IWorkflowTemplateRepository
{
    void Create(WorkflowTemplate workflowTemplate);
    WorkflowTemplate? TryGetById(Guid workflowTemplateId);
    IEnumerable<WorkflowTemplate> GetAll();
}