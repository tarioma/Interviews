using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.GetEmployeeById;

public class GetEmployeeByIdHandler : Handler<GetEmployeeByIdQuery, Employee>
{
    public GetEmployeeByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Employee Handle(GetEmployeeByIdQuery command) =>
        TenantFactory.Employees.TryGetById(command.EmployeeId)
        ?? throw new Exception($"Нет {nameof(Employee)} с таким id.");
}