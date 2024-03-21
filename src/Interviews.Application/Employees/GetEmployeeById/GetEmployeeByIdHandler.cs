using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.GetEmployeeById;

public class GetEmployeeByIdHandler(ITenantRepository tenant) : Handler(tenant)
{
    public Employee Handle(GetEmployeeByIdQuery query)
    {
        Guard.Against.Null(query);

        return Tenant.Employees.GetById(query.EmployeeId);
    }
}