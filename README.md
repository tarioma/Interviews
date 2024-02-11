```mermaid
classDiagram
    User --> Role
    User --> EmailAddress
    Request --> Document
    Request --> Workflow
    Request --> IRequestEvent
    Document --> EmailAddress
    IRequestEvent --> RequestCreateEvent
    IRequestEvent --> RequestApprovedEvent
    IRequestEvent --> RequestRejectEvent
    Workflow --> WorkflowStep
    WorkflowTemplate --> WorkflowStepTemplate

    class User{
        Id
        Name
        EmailAddress
        RoleId
    }
    class Role{
        Id
        Name
    }
    class EmailAddress{
        Value
    }
    class Request{
        Id
        UserId -- кто
        Document 
        Workflow
        Events IRequestEvent[]
        
        IsApproved()
        IsReject()
        Approve(User)
        Reject(User)
        Restart()
    }
    class IRequestEvent{
        Id
        UtcDateTime
        RequestId
    }
    class RequestCreateEvent{
        
    }
    class RequestApprovedEvent{
        
    }
    class RequestRejectEvent{
        
    }
    class Document{
        Name
        DateOfBirth
        EmailAddress
    }
    class Workflow{
        WorkflowTemplateId
        Name
        Steps WorkflowStep[]
        
        IsApproved()
        IsRejected()
        Approve()
        Reject()
    }
    class WorkflowStep{
        Name
        Order
        Comment
        Status?
        UserId?
        RoleId?
        
        SetStatus(Status)
    }
    class WorkflowTemplate{
        Id
        Name
        Steps WorkflowStepTemplate[]
        Create(User, Document) Request
    }

    class WorkflowStepTemplate{
        Name
        Order
        UserId?
        RoleId?
    }
```
