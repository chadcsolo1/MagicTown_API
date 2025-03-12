namespace MagicTown_TownAPI.Models
{
    public class Town
    {
        public int Id { get; set; }
        public string Name { get; set; }  = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
