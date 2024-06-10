using Domain.Common;
using Domain.Shared;


namespace Domain.Interface
{
    public interface IUnitOfWork<DBContext>: IDisposable
    {
        IRepository<DBContext, T> GetRepository<T>() where T : BaseEntity, IAggregateRoot;
        Task Commit();
    }
}
