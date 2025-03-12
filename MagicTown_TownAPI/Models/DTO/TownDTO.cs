using System.ComponentModel.DataAnnotations;

namespace MagicTown_TownAPI.Models.DTO
{
    public class TownDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public int Population { get; set; }
        public int AverageIncome { get; set; }
    }
}
