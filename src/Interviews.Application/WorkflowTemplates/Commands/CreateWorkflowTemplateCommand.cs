using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Application.WorkflowTemplates.Commands;

public record CreateWorkflowTemplateCommand(string Name, IReadOnlyCollection<WorkflowStepTemplate> Steps);