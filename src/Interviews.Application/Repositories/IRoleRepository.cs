using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Repositories;

public interface IRoleRepository
{
    void Create(Role role);
    void Update(Role role);
    void Delete(Guid roleId);
    Role GetById(Guid roleId);
    IEnumerable<Role> GetAll();
}