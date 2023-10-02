using Resumes.Domain.Exceptions;

namespace Resumes.Domain.InterviewRequest;

public class WorkflowRequest
{
    public Guid WorkflowTemplateId { get; private init; }
    public string Name { get; private init; }
    public HashSet<WorkflowStep> Steps { get; private init; }

    public WorkflowRequest(Guid workflowTemplateId, string name, HashSet<WorkflowStep> steps)
    {
        EmptyGuidException.ThrowIfEmpty(workflowTemplateId);
        ArgumentException.ThrowIfNullOrEmpty(name);

        WorkflowTemplateId = workflowTemplateId;
        Name = name;
        Steps = steps;
    }
}