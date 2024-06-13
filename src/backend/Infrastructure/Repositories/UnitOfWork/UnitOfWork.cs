using Application.Common.Interface;
using Domain.Common;
using Domain.Interface;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
                //ChangeModified();
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
        //private void ChangeModified()
        //{
        //    var trackerEntity = _dbContext.ChangeTracker.Entries().Where(x => x is IDatedModification).ToList();
        //    trackerEntity.ForEach(x =>
        //    {
        //        ((IDatedModification)x.Entity).UpdatedAt = DateTime.Now;
        //        if (x.State == EntityState.Added)
        //        {
        //            ((IDatedModification)x.Entity).CreatedAt = DateTime.Now;
        //        }
        //    }
        //    );

        //}
        public IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot
        {
            return _serviceProvider.GetService<IRepository<T>>() ?? throw new ArgumentNullException();
        }
    }
}
