using Resumes.Domain.Entities.Exceptions;
using Resumes.Domain.Entities.Requests.Events;

namespace Resumes.Domain.Entities.Requests;

public class Request
{
    private readonly HashSet<IEvent> _events;
    
    public Guid Id { get; }
    public User User { get; private set; } = null!;
    public Document Document { get; private set; } = null!;
    public Workflow Workflow { get; private set; } = null!;
    public IReadOnlyCollection<IEvent> Events => _events;

    private Request(Guid id, User user, Document document, Workflow workflow)
    {
        EmptyGuidException.ThrowIfEmpty(id);

        _events = new HashSet<IEvent>();
        
        Id = id;
        SetUser(user);
        SetDocument(document);
        SetWorkflow(workflow);
    }

    public static Request Create(User user, Document document, Workflow workflow)
    {
        var id = Guid.NewGuid();
        return new Request(id, user, document, workflow);
    }
    
    public void SetUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        User = user;
    }
    
    public void SetDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);
        Document = document;
    }
    
    public void SetWorkflow(Workflow workflow)
    {
        ArgumentNullException.ThrowIfNull(workflow);
        Workflow = workflow;
    }

    public void Approve()
    {
        var statusPending = Workflow.Steps.FirstOrDefault(step => step.Status == Status.Pending);
        ArgumentNullException.ThrowIfNull(statusPending, "Не найдено состояние ожидания");

        statusPending.Status = Status.Approve;
        _events.Add(RequestApprovedEvent.Create(Id));
    }
    
    public void Reject()
    {
        var statusPending = Workflow.Steps.FirstOrDefault(step => step.Status == Status.Pending);
        ArgumentNullException.ThrowIfNull(statusPending, "Не найдено состояние ожидания");

        statusPending.Status = Status.Reject;
        _events.Add(RequestRejectedEvent.Create(Id));
    }
    
    public void Restart()
    {
        foreach (var step in Workflow.Steps)
        {
            step.Status = Status.Pending;
        }
    }
}