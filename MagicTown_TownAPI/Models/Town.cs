using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicTown_TownAPI.Models
{
    public class Town
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public string Name { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BiggestAttraction { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Population { get; set; }
        public double AverageIncome { get; set; }   
        public DateTime DateCreated { get; set; }
        public DateTime UpdateDate { get; set; }    
    }
}
