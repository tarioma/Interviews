namespace Interviews.Application.Roles.Commands;

public record UpdateRoleNameCommand(Guid RoleId, string Name);