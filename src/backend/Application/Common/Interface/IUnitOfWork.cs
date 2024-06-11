using Domain.Common;
using Domain.Interface;
using Domain.Shared;


namespace Application.Common.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot;
        Task Commit();
    }
}
