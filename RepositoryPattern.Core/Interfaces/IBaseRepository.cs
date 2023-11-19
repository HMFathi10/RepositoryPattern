using RepositoryPattern.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? Get(int id);
        IEnumerable<T> GetAll();
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, string[]? includes = null);
        IEnumerable<T?> FindAll(Expression<Func<T, bool>> predicate, string[]? includes = null);
        IEnumerable<T?> FindAll(Expression<Func<T, bool>> predicate, 
            int? skip, int? take, Expression<Func<T, object>>? orderBy = null, string? orderByDirection = OrderBy.Ascending);
        T? Add(T entity);
        IEnumerable<T?> AddRange(IEnumerable<T> entities);
    }
}
