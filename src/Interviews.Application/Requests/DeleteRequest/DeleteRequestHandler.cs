using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application.Requests.DeleteRequest;

public class DeleteRequestHandler(ITenantRepository tenant) : Handler(tenant)
{
    public void Handle(DeleteRequestCommand command)
    {
        Guard.Against.Null(command);

        Tenant.Employees.Delete(command.RoleId);
        Tenant.Commit();
    }
}