using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.LoginEmployee;

public record LoginEmployeeCommand(EmailAddress EmailAddress, AuthData AuthData);