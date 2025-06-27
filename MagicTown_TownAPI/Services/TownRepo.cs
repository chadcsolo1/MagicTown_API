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

        public IEnumerable<Town> GetAllTownsTest(string filter, string orderBy, int pageSize, int pageNumber)
        {
            IQueryable<Town> query = _db.Towns;

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter.Contains("Name"))
                {
                    query = query.Where(t => t.Name.Contains(filter));
                }else if (filter.Contains("Description"))
                {
                    query = query.Where(t => t.Description.Contains(filter));
                } else if (filter.Contains("Population") && filter.Contains(">"))
                {
                    query = query.Where(t => t.Population > Int32.Parse(filter));
                } else if (filter.Contains("Population") && filter.Contains("<"))
                {
                    query = query.Where(t => t.Population < Int32.Parse(filter));
                } else if (filter.Contains("AverageIncome") && filter.Contains("<"))
                {
                    query = query.Where(t => t.AverageIncome < Int32.Parse(filter));
                } else if (filter.Contains("AverageIncome") && filter.Contains(">"))
                {
                    query = query.Where(t => t.AverageIncome > Int32.Parse(filter));
                }
                
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.Contains("Name"))
                {
                    query = query.OrderBy(t => t.Name);
                } else if (orderBy.Contains("Population"))
                {
                    query = query.OrderBy(t => t.Population);
                } else if (orderBy.Contains("AverageIncome"))
                {
                    query = query.OrderBy(t => t.AverageIncome);
                }
            }

            return (pageNumber != null && pageSize != null) ? 
                query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
            //return _db.Towns.ToList();
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
