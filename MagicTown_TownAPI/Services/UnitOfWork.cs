using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;

namespace MagicTown_TownAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private ITownRepo _townRepo;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }
        public ITownRepo townRepo
        {
            get
            {
                return _townRepo = _townRepo ?? new TownRepo(_db);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
