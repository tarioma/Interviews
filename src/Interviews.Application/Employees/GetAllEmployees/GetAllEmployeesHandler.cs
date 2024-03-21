using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.GetAllEmployees;

public class GetAllEmployeesHandler(ITenantRepository tenant) : Handler(tenant)
{
    public IEnumerable<Employee> Handle(GetAllEmployeesQuery query)
    {
        Guard.Against.Null(query);

        return Tenant.Employees.GetAll();
    }
}