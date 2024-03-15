namespace Interviews.Application.Employees.Commands;

public record UpdateEmployeeNameCommand(Guid EmployeeId, string Name);