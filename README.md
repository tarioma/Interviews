```mermaid
classDiagram
    Employee --> Role
    Request --> Document
    Request --> Workflow
    Request --> RequestEvent
    RequestEvent --> RequestCreatedEvent
    RequestEvent --> RequestApprovedEvent
    RequestEvent --> RequestRejectEvent
    RequestEvent --> RequestRestartedEvent
    RequestEvent --> RequestNextStepEvent
    Workflow --> WorkflowStep
    WorkflowTemplate --> WorkflowStepTemplate

    class Employee{
        Id
        Name
        EmailAddress
        RoleId
    }
    class Role{
        Id
        Name
    }
    class Request{
        Id
        Document 
        Workflow
        EmployeeId
        Events RequestEvent[]
        
        IsApproved()
        IsReject()
        Approve(Employee, comment?)
        Reject(Employee, comment?)
        Restart(Document)
    }
    class RequestEvent{
        Id
        DateTime
        RequestId
    }
    class RequestCreatedEvent{
    }
    class RequestApprovedEvent{
    }
    class RequestRejectEvent{
    }
    class RequestRestartedEvent{
    }
    class RequestNextStepEvent{
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
        Approve(Employee, comment?)
        Reject(Employee, comment?)
        Restart(Employee)
    }
    class WorkflowStep{
        Name
        Order
        EmployeeId?
        RoleId?
        Status?
        Comment
        
        Approve(Employee, comment?)
        Reject(Employee, comment?)
        ToPending()
    }
    class WorkflowTemplate{
        Id
        Name
        Steps WorkflowStepTemplate[]
        CreateRequest(Employee, Document) Request
    }

    class WorkflowStepTemplate{
        Name
        Order
        EmployeeId
        RoleId
    }
```
