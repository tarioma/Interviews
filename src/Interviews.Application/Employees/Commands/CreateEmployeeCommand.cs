namespace Interviews.Application.Employees.Commands;

public record CreateEmployeeCommand(string Name, string EmailAddress, Guid RoleId);