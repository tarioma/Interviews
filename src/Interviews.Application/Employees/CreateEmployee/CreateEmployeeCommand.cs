using Interviews.Domain.Entities;

namespace Interviews.Application.Employees.CreateEmployee;

public record CreateEmployeeCommand(string Name, EmailAddress EmailAddress, Guid RoleId);