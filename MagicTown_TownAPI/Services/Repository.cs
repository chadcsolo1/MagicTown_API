using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;

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

            if(entity == null) { throw new Exception($"The entity was does not exist based on the {id} you provided");}
            return entity;
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
            //_unitOfWork.Save();
            
        }
    }
}
