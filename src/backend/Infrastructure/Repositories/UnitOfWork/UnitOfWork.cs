using Domain.Common;
using Domain.Interface;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
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
            throw new NotImplementedException();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}
