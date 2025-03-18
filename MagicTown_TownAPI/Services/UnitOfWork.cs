using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using MagicTown_TownAPI.Models;

namespace MagicTown_TownAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        //private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly ApplicationDbContext _context;

        public IRepository<Town> townRepo;
        public IRepository<House> houseRepo;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Town> TownRepo
        {
            get
            {
                if (this.townRepo == null)
                {
                    this.townRepo = new Repository<Town>(_context);
                }
                return townRepo;
            }
        }

        public IRepository<House> HouseRepo
        {
            get
            {
                if (this.houseRepo == null)
                {
                    this.houseRepo = new Repository<House>(_context);
                }
                return houseRepo;
            }
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
