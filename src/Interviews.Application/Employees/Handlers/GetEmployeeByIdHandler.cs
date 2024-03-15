using Ardalis.GuardClauses;
using Interviews.Application.Employees.Queries;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.Handlers;

public class GetEmployeeByIdHandler
{
    private readonly ITenantRepository _tenant;

    public GetEmployeeByIdHandler(ITenantRepository tenant)
    {
        Guard.Against.Null(tenant);

        _tenant = tenant;
    }

    public Employee Handle(GetEmployeeByIdQuery query)
    {
        Guard.Against.Null(query);

        return _tenant.Employees.GetById(query.EmployeeId);
    }
}