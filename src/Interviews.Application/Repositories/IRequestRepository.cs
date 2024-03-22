using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Repositories;

public interface IRequestRepository
{
    void Create(Request request);
    Request? TryGetById(Guid requestId);
    IEnumerable<Request> GetAll();
}