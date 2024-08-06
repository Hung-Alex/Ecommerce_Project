using Application.Common.Interface;
using Domain.Common;
using Domain.Interface;
using Domain.Shared;
using Infrastructure.Persistence.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentUserService _currentUserService;
        public UnitOfWork(StoreDbContext dbContext, IServiceProvider serviceProvider, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _currentUserService = currentUserService;
        }
        public async Task Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
