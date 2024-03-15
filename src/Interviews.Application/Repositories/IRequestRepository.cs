using Interviews.Domain.Entities.Requests;

namespace Interviews.Application.Repositories;

public interface IRequestRepository
{
    void Create(Request request);
    void Delete(Request request);
    Request GetById(Guid id);
    IReadOnlySet<Request> GetAll();
}