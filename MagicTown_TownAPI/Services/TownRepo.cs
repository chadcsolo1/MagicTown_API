using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using MagicTown_TownAPI.Models;
using Microsoft.IdentityModel.Tokens;
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
            //Dictionary<string,string> fspTwo = new Dictionary<string,string>();
            

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
                    var amountTest = Int32.Parse(fsp["Number"]);
                    //query = query.Where(t => t.Population > Int32.Parse(fsp["Population"]));
                    query = query.Where(t => t.Population > Int32.Parse(fsp["Number"]));
                } 
                if (fsp.ContainsKey("Population") && fsp["Population"].Contains("<"))
                {
                    //query = query.Where(t => t.Population < Int32.Parse(fsp["Population"]));
                    query = query.Where(t => t.Population < Int32.Parse(fsp["Number"]));
                } 
                if (fsp.ContainsKey("AverageIncome") && fsp["AverageIncome"].Contains("<"))
                {
                    //query = query.Where(t => t.AverageIncome < Int32.Parse(fsp["AverageIncome"]));
                    query = query.Where(t => t.AverageIncome < Int32.Parse(fsp["Number"]));
                } 
                if (fsp.ContainsKey("AverageIncome") && fsp["AverageIncome"].Contains(">"))
                {
                    query = query.Where(t => t.AverageIncome > Int32.Parse(fsp["Number"]));
                }
                
            }

            //if (!string.IsNullOrEmpty(orderBy))
            //{
            //     fspTwo = JsonSerializer.Deserialize<Dictionary<string,string>>(orderBy);

            //    if (orderBy.Contains("Name"))
            //    {
            //        if (fspTwo["Name"].Contains("DESC"))
            //        {
            //            query = query.OrderByDescending(t => t.Name);
            //        } else
            //        {
            //            query = query.OrderBy(t => t.Name);
            //        }
                    
            //    } 
            //    if (orderBy.Contains("Population"))
            //    {
            //        if (fspTwo["Population"].Contains("DESC"))
            //        {
            //            query = query.OrderByDescending(t => t.Population);
            //        } else
            //        {
            //            query = query.OrderBy(t => t.Population);
            //        }
                    
            //    } 
            //    if (orderBy.Contains("AverageIncome"))
            //    {
            //        if (fspTwo["AverageIncome"].Contains("DESC"))
            //        {
            //            query = query.OrderByDescending(t => t.AverageIncome);
            //        } else
            //        {
            //            query = query.OrderBy(t => t.AverageIncome);
            //        }
            //    }
            //}

            //towns = (pageNumber != null && pageSize != null) ? 
            //    query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList() : query.ToList();

            towns = query.ToList();

            

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.Contains("DESC"))
                {
                    if (orderBy.Contains("Name"))
                    {
                        towns = towns.OrderByDescending(t => t.Name).ToList();
                    } else if (orderBy.Contains("Population"))
                    {
                        towns = towns.OrderByDescending(t => t.Population).ToList();
                    } else if (orderBy.Contains("AverageIncome"))
                    {
                        towns = towns.OrderByDescending(t => t.AverageIncome).ToList();
                    }

                } else
                {
                    if (orderBy.Contains("Name"))
                    {
                        towns = towns.OrderBy(t => t.Name).ToList();
                    } else if (orderBy.Contains("Population"))
                    {
                        towns = towns.OrderBy(t => t.Population).ToList();
                    } else if (orderBy.Contains("AverageIncome"))
                    {
                        towns = towns.OrderBy(t => t.AverageIncome).ToList();
                    }
                }
            }

            if (pageNumber != null && pageSize != null)
            {
                towns = towns.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            }



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
