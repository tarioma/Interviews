using Interviews.Domain.Entities.Employees;

namespace Interviews.Application.Repositories;

public interface IRoleRepository
{
    void Create(Role role);
    Role? TryGetById(Guid roleId);
    IEnumerable<Role> GetAll();
}