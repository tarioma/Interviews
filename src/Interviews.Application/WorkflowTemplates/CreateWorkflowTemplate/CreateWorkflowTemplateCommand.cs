using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.CreateWorkflowTemplate;

public record CreateWorkflowTemplateCommand(string Name, IReadOnlyCollection<WorkflowStepTemplate> Steps);