using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;

namespace MagicTown_TownAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        //private ITownRepo _townRepo;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
