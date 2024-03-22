using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Repositories;

public interface IEmployeeRepository
{
    void Create(Employee employee);
    Employee? TryGetById(Guid employeeId);
    Employee? TryGetByEmail(EmailAddress emailAddress);
    IEnumerable<Employee> GetAll();
}