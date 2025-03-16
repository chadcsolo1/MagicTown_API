using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using MagicTown_TownAPI.Models;

namespace MagicTown_TownAPI.Services
{
    public class TownRepo : ITownRepo
    {
        private readonly ApplicationDbContext _db;
        public TownRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void CreateTown(Town town)
        {
            _db.Add(town);
        }

        public void DeleteTown(int id)
        {
            _db.Remove(id);
        }

        public List<Town> GetAllTowns()
        {
            return _db.Towns.ToList();
        }

        public Town GetTown(int id)
        {
            return _db.Towns.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateTown(Town town)
        {
            _db.Update(town);
        }
    }
}
