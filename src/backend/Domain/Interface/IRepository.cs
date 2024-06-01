using Domain.Shared;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Domain.Interface
{
    public interface IRepository<T> where T : BaseEntity,IAggregateRoot
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> FindOneAsync(BaseSpecification<T> spec,CancellationToken cancellationToken=default); 
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(BaseSpecification<T> spec, CancellationToken cancellationToken = default);
    }
}
