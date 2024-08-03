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
            var user = _currentUserService.GetCurrentUser().Data?.Id;
            var entries = _dbContext.ChangeTracker
        .Entries()
        .Where(e =>
                (e.Entity is IDatedModification
                || e.Entity is ICreatedAndUpdatedBy
                || e.Entity is ISoftDelete)
                &&
                (e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted
                ));

            foreach (var entityEntry in entries)
            {
                var datedEntity = entityEntry.Entity as IDatedModification;
                var createdUpdatedEntity = entityEntry.Entity as ICreatedAndUpdatedBy;
                var deleteEntity = entityEntry.Entity as ISoftDelete;
                if (datedEntity != null)
                {
                    datedEntity.UpdatedAt = DateTimeOffset.Now;
                    if (entityEntry.State == EntityState.Added)
                    {
                        datedEntity.CreatedAt = DateTimeOffset.Now;
                    }
                }
                if (deleteEntity != null)
                {

                    if (entityEntry.State == EntityState.Deleted)
                    {
                        entityEntry.State = EntityState.Modified;
                        deleteEntity.IsDeleted = true;
                        deleteEntity.DeletedAt = DateTimeOffset.Now;
                    }
                }

                if (createdUpdatedEntity != null)
                {
                    createdUpdatedEntity.UpdatedByUserId = user;
                    if (entityEntry.State == EntityState.Added)
                    {
                        createdUpdatedEntity.CreatedByUserId = user;
                    }
                }
            }
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot
        {
            return _serviceProvider.GetService<IRepository<T>>() ?? throw new ArgumentNullException();
        }
    }
}
