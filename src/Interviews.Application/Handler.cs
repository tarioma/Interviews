using Ardalis.GuardClauses;
using Interviews.Application.Repositories;

namespace Interviews.Application;

public abstract class Handler<TRequest, TResult>
{
    protected readonly ITenant TenantFactory;

    protected Handler(ITenantFactory tenantFactory)
    {
        Guard.Against.Null(tenantFactory);

        TenantFactory = tenantFactory.GetTenant();
    }

    public abstract TResult Handle(TRequest command);
}