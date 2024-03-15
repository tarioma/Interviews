using Ardalis.GuardClauses;
using Interviews.Application.Employees.Queries;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.Handlers;

public class GetAllEmployeesHandler
{
    private readonly ITenantRepository _tenant;

    public GetAllEmployeesHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public IReadOnlySet<Employee> Handle(GetAllEmployeesQuery query)
    {
        Guard.Against.Null(query);

        return _tenant.Employees.GetAll();
    }
}