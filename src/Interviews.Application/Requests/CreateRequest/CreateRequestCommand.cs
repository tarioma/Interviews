using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Requests.CreateRequest;

public record CreateRequestCommand(Document Document, Workflow Workflow, Guid EmployeeId);