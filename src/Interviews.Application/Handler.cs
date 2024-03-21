using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application;

public abstract class Handler
{
    protected readonly ITenantRepository Tenant;

    protected Handler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        Tenant = tenant;
    }
}