using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Repositories;

public interface IEmployeeRepository
{
    void Create(Employee employee);
    void Update(Employee employee);
    void Delete(Employee employee);
    Employee GetById(Guid id);
    IReadOnlySet<Employee> GetAll();
}