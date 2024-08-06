using Application.Common.Interface;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor
{
    public class UpdateAuditableInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentUserService _currentUserService;
        public UpdateAuditableInterceptor(IServiceProvider serviceProvider, ICurrentUserService currentUserService)
        {
            _serviceProvider = serviceProvider;
            _currentUserService = currentUserService;
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {

            if (eventData.Context is not null)
            {
                ChangeModified(eventData.Context);
            }
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        private void ChangeModified(DbContext context)
        {
            var user = _currentUserService.GetCurrentUser().Data?.Id;
            var entries = context.ChangeTracker
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
    }
}
