using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using MagicTown_TownAPI.Models;
using System.Text.Json;

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

        public IEnumerable<Town> GetAllTownsTest(string filter, string orderBy, int? pageSize = null, int? pageNumber = null)
        {
            //IQueryable<Town> query = _db.Set<Town>();
            IQueryable<Town> query = _db.Towns;
            IEnumerable<Town> towns = new List<Town>();

            Dictionary<string,string> fsp = new Dictionary<string,string>();

            if (!string.IsNullOrEmpty(filter))
            {
                fsp = JsonSerializer.Deserialize<Dictionary<string,string>>(filter);
                //filter.Contains("Name")
                if (fsp.ContainsKey("Name"))
                {
                    var name = fsp["Name"];
                    query = query.Where(t => t.Name.Contains(name));
                    //towns = query.Where(t => t.Name.Contains(name)).ToList();

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

            //towns = (pageNumber != null && pageSize != null) ? 
            //    query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList() : query.ToList();

            towns = query.ToList();
            return towns;

            //return (pageNumber != null && pageSize != null) ? 
            //    query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value) : query;
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
