using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.DataLayer.Data
{
    public interface IEntity<T> where T : class
    {
        Task<List<T>> List();

        Task<IList<T>> List(int? page, int? pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> sort);

        Task<int> Add(T item);

        Task<bool> BulkAdd(List<T> item);

        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        Task<int> Update(T item);

        Task<bool> Delete(T Item);

        Task<bool> BulkDelete(List<T> item);
    }
}
