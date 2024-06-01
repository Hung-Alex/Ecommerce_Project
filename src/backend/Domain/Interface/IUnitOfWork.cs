using Domain.Common;
using Domain.Shared;


namespace Domain.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot;
        Task Commit();
    }
}
