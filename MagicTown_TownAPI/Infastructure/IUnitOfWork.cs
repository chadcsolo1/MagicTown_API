using MagicTown_TownAPI.Data;

namespace MagicTown_TownAPI.Infastructure
{
    public interface IUnitOfWork
    {
        //private ApplicationDbContext context = new ApplicationDbContext();
        void Save();
        void Rollback();
    }
}
