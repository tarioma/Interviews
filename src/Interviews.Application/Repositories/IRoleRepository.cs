using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Repositories;

public interface IRoleRepository
{
    void Create(Role role);
    void Update(Role role);
    void Delete(Role role);
    Role GetById(Guid id);
    IReadOnlySet<Role> GetAll();
}