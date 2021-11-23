﻿using Microsoft.EntityFrameworkCore;
using NLog;
using Zoo_Management.Models.Database;

namespace Zoo_Management
{
    public class ZooDbContext : DbContext
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
        {
        }

        public DbSet<Species> Species { get; set; }
        public DbSet<Animal> Animals { get; set; }
    }
}
