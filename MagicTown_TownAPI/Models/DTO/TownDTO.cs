using System.ComponentModel.DataAnnotations;

namespace MagicTown_TownAPI.Models.DTO
{
    public class TownDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BiggestAttraction { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Population { get; set; }
        public double AverageIncome { get; set; }
    }
}
