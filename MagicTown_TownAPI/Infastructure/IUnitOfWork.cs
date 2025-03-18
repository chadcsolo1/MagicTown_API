using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Models;

namespace MagicTown_TownAPI.Infastructure
{
    public interface IUnitOfWork
    {
        IRepository<Town> TownRepo { get; }
        IRepository<House> HouseRepo { get; }
        void Save();
        void Rollback();
    }
}
