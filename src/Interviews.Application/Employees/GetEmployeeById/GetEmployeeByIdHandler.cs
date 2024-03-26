using Ardalis.GuardClauses;
using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.GetEmployeeById;

public class GetEmployeeByIdHandler : Handler<GetEmployeeByIdQuery, Employee>
{
    public GetEmployeeByIdHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override Employee Handle(GetEmployeeByIdQuery command)
    {
        Guard.Against.Null(command);

        var employee = TenantFactory.Employees.TryGetById(command.EmployeeId);

        if (employee is null)
        {
            throw new Exception($"Нет {nameof(Employee)} с таким id.");
        }

        return employee;
    }
}