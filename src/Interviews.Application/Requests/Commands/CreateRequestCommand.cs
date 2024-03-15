using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.Commands;

public record CreateRequestCommand(Guid WorkflowTemplateId, Guid EmployeeId, Document Document);