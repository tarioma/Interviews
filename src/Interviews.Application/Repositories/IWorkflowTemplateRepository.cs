using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.Repositories;

public interface IWorkflowTemplateRepository
{
    void Create(WorkflowTemplate workflowTemplate);
    void Update(WorkflowTemplate workflowTemplate);
    void Delete(Guid workflowTemplateId);
    WorkflowTemplate GetById(Guid workflowTemplateId);
    IEnumerable<WorkflowTemplate> GetAll();
}