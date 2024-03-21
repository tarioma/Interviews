using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Repositories;

public interface IEmployeeRepository
{
    void Create(Employee employee);
    void Update(Employee employee);
    void Delete(Guid employeeId);
    Employee GetById(Guid employeeId);
    IEnumerable<Employee> GetAll();
}