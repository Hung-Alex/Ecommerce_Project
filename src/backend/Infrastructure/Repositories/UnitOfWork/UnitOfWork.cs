using Application.Common.Interface;
using Domain.Common;
using Domain.Interface;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(StoreDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }
        public async Task Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("error with save changes");
            }
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot
        {
            return _serviceProvider.GetService<IRepository<T>>() ?? throw new ArgumentNullException();
        }
    }
}
