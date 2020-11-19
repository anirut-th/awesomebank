using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Repository.Interface
{
    public interface IGenericRepository<T>
    {
        public IEnumerable<T> GetAll();
        public T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        public IEnumerable<T> GetMultiple(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        public T GetById(Guid Id);
        public int Add(T model);
        public int Update(T model);
        public int Remove(Guid Id);
    }
}
