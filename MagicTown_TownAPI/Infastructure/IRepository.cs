using MagicTown_TownAPI.Models;
using System.Linq.Expressions;

namespace MagicTown_TownAPI.Infastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageSize = null, int? pageNumber = null);
        //IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null);
        T Get(int id);
        IEnumerable<T> GetAllNoFilter();
        IEnumerable<T> GetAllTownsTest(string filter, string orderBy, int pageSize, int pageNumber);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
