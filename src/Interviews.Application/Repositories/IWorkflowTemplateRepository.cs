using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.Repositories;

public interface IWorkflowTemplateRepository
{
    void Create(WorkflowTemplate workflowTemplate);
    void Delete(WorkflowTemplate workflowTemplate);
    WorkflowTemplate GetById(Guid id);
    IReadOnlySet<WorkflowTemplate> GetAll();
}