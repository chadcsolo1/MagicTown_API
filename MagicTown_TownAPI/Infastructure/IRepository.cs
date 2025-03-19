using MagicTown_TownAPI.Models;
using System.Linq.Expressions;

namespace MagicTown_TownAPI.Infastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? pageSize = null, int? pageNumber = null);
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
