using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicTown_TownAPI.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        //protected readonly IUnitOfWork _unitOfWork;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_unitOfWork = UnitOfWork;
        }
        public void Create(T entity)
        {
            if(entity == null) {throw new Exception($"Create Method in Repository - the entity was null {entity}");}
            _db.Set<T>().Add(entity);
            //_unitOfWork.Save();
        }

        public void Delete(T entity)
        {            
            _db.Set<T>().Remove(entity);
            //_unitOfWork.Save();
        }

        public T Get(int id)
        {
            var entity = _db.Set<T>().Find(id);

            if (entity == null)
            {
                throw new Exception($"The entity was does not exist based on the {id} you provided");
            }
            return entity;
        }

        public IEnumerable<T> GetAllNoFilter()
        {
            return _db.Set<T>().ToList() ?? throw new Exception("No entities found in the database.");
        }

        //public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        //{
        //    IQueryable<T> query = _db.Set<T>();

        //    if(filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    return query.ToList();

        //}


        //GetAll(
        //    Expression<Func<T, bool>>? filter = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    int? pageSize = null, int? pageNumber = null
        //    )

        //public IEnumerable<T> GetAllTest(string filter, string orderBy, int pageSize, int pageNumber)
        //{
        //    var town = _db.Set<T>().Where()

        //    if (!string.IsNullOrEmpty(filter))
        //    {
        //        town = town.Where(x => );
        //        //town = town.Where(t => EF.Functions.Like(t.ToString(), $"%{filter}%"));
        //    }
        //}

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageSize = null, int? pageNumber = null
            )
        {
            IQueryable<T> query = _db.Set<T>();

            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);

            return (pageNumber != null && pageSize != null) ?
                query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value) : query;

        }


        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
            //_unitOfWork.Save();
            
        }
    }
}
