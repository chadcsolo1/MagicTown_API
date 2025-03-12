using MagicTown_TownAPI.Models.DTO;

namespace MagicTown_TownAPI.Data
{
    public class TownStore
    {
        public static List<TownDTO> townList = new List<TownDTO>
        {
                new TownDTO { Id = 1, Name = "Mountain Town"},
                new TownDTO { Id = 2, Name = "Mystic Town"}
        };
    }
}
