﻿using Interviews.Application.Repositories;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Employees.GetAllEmployees;

public class GetAllEmployeesHandler : Handler<GetAllEmployeesQuery, IEnumerable<Employee>>
{
    public GetAllEmployeesHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public override IEnumerable<Employee> Handle(GetAllEmployeesQuery command) =>
        TenantFactory.Employees.GetAll();
}