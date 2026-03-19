using E5MakersMarkt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace E5MakersMarkt.Data
{
    class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;" +
                "database=" +
                "MakersMarkt;" +
                "user=root;" +
                "password=;",
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin123") }
            );
        }
    }
}
