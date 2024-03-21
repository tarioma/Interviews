using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Repositories;

public interface IRequestRepository
{
    void Create(Request request);
    void Update(Request request);
    void Delete(Guid requestId);
    Request GetById(Guid requestId);
    IEnumerable<Request> GetAll();
}