namespace Interviews.Application.Employees.Commands;

public record UpdateEmployeeEmailAddressCommand(Guid EmployeeId, string EmailAddress);