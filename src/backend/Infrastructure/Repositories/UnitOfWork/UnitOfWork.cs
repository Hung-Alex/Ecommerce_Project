using Application.Common.Interface;
using Domain.Common;
using Domain.Interface;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories.UnitOfWork
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
                ChangeModified();
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
        private void ChangeModified()
        {
            var useId = _currentUserService.GetCurrentUser().Data.Id;
            var entries = _dbContext.ChangeTracker
        .Entries()
        .Where(e => e.Entity is IDatedModification && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IDatedModification)entityEntry.Entity).UpdatedAt = DateTimeOffset.Now;
                if (entityEntry is ICreatedAndUpdatedBy)
                {
                    ((ICreatedAndUpdatedBy)entityEntry.Entity).CreatedByUserId = useId;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((IDatedModification)entityEntry.Entity).CreatedAt = DateTimeOffset.Now;
                    ((ICreatedAndUpdatedBy)entityEntry.Entity).UpdatedByUserId = useId;
                }
            }
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot
        {
            return _serviceProvider.GetService<IRepository<T>>() ?? throw new ArgumentNullException();
        }
    }
}
