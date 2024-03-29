﻿namespace Interviews.Application.Repositories;

public interface ITenant
{
    IEmployeeRepository Employees { get; }
    IRequestRepository Requests { get; }
    IRoleRepository Roles { get; }
    IWorkflowTemplateRepository WorkflowTemplates { get; }

    void Commit();
}