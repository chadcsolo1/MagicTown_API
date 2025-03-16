using MagicTown_TownAPI.Models;

namespace MagicTown_TownAPI.Infastructure
{
    public interface ITownRepo
    {
        List<Town> GetAllTowns();
        Town GetTown(int id);
        void CreateTown(Town town);
        void UpdateTown(Town town);
        void DeleteTown(int id);
    }
}
