using MagicTown_TownAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTown_TownAPI.Data
{
    public class ApplicationDbContext<T> : DbContext where T : class
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext<T>> options) : base(options) { }
        public DbSet<T> entity { get; set; }
    }
}
