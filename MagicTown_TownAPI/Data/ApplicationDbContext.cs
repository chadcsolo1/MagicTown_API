﻿using MagicTown_TownAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTown_TownAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //public ApplicationDbContext() : base("DefaultConnection");
        public DbSet<Town> Towns { get; set; }
        public DbSet<House> Houses { get; set; }

    }
}
