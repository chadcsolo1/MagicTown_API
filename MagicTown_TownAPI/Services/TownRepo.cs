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
                
                if (fsp.ContainsKey("Name"))
                {
                    query = query.Where(t => t.Name.Contains(fsp["Name"]));

                }
                if (fsp.ContainsKey("Description"))
                {
                    query = query.Where(t => t.Description.Contains(fsp["Description"]));
                }
                if (fsp.ContainsKey("Population") && fsp["Population"].Contains(">"))
                {
                    query = query.Where(t => t.Population > Int32.Parse(fsp["Population"]));
                } 
                if (fsp.ContainsKey("Population") && fsp["Population"].Contains("<"))
                {
                    query = query.Where(t => t.Population < Int32.Parse(fsp["Population"]));
                } 
                if (fsp.ContainsKey("AverageIncome") && fsp["AverageIncome"].Contains("<"))
                {
                    query = query.Where(t => t.AverageIncome < Int32.Parse(fsp["AverageIncome"]));
                } 
                if (fsp.ContainsKey("AverageIncome") && fsp["AverageIncome"].Contains(">"))
                {
                    query = query.Where(t => t.AverageIncome > Int32.Parse(fsp["AverageIncome"]));
                }
                
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                fsp = JsonSerializer.Deserialize<Dictionary<string,string>>(orderBy);

                if (orderBy.Contains("Name"))
                {
                    if (fsp["Name"].Contains("DESC"))
                    {
                        query = query.OrderByDescending(t => t.Name);
                    } else
                    {
                        query = query.OrderBy(t => t.Name);
                    }
                    
                } 
                if (orderBy.Contains("Population"))
                {
                    if (fsp["Population"].Contains("DESC"))
                    {
                        query = query.OrderByDescending(t => t.Population);
                    } else
                    {
                        query = query.OrderBy(t => t.Population);
                    }
                    
                } 
                if (orderBy.Contains("AverageIncome"))
                {
                    if (fsp["AverageIncome"].Contains("DESC"))
                    {
                        query = query.OrderByDescending(t => t.AverageIncome);
                    } else
                    {
                        query = query.OrderBy(t => t.AverageIncome);
                    }
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
