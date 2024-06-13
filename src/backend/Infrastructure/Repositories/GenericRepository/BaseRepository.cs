using Domain.Common;
using Domain.Interface;
using Domain.Shared;
using Domain.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.GenericRepository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly StoreDbContext _context;
        public BaseRepository(StoreDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<T> FindOneAsync(BaseSpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var result = GetQuery(_context.Set<T>(), spec);
            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(BaseSpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var result = GetQuery(_context.Set<T>(), spec);
            return await result.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<long> CountAsync(BaseSpecification<T> spec, CancellationToken cancellationToken = default)
        {
            spec.IsPagingEnabled = false;
            var result = GetQuery(_context.Set<T>(), spec);
            return await result.LongCountAsync(cancellationToken);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        private static IQueryable<T> GetQuery(IQueryable<T> inpuQuery, BaseSpecification<T> spec)
        {
            var query = inpuQuery;
            if (!(spec.Criteria is null))
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            if (!(spec.OrderBy is null))
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (!(spec.OrderByDescending is null))
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (!(spec.GroupBy is null))
            {
                query = query.GroupBy(spec.GroupBy).SelectMany(x => x);
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip)
                    .Take(spec.Take);
            }
            return query;
        }
    }
}
