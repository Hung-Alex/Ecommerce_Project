using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<T>
    {
        void Add(T Entity);
        void Delete(T Entity);
        void Update(T Entity);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> cretial);
    }
}
