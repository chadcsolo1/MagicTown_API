using MagicTown_TownAPI.Models.DTO;

namespace MagicTown_TownAPI.Data
{
    public class TownStore
    {
        public static List<TownDTO> townList = new List<TownDTO>
        {
                new TownDTO { Id = 1, Name = "Mountain Town", Population = 2000, AverageIncome = 2000},
                new TownDTO { Id = 2, Name = "Mystic Town", Population = 10000, AverageIncome = 2500}
        };
    }
}
