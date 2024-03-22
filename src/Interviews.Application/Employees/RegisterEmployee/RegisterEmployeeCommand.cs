using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.RegisterEmployee;

public record RegisterEmployeeCommand(string Name, EmailAddress EmailAddress, Guid RoleId, AuthData AuthData);