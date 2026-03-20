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
        
        public DbSet<Product> Products { get; set; }
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
                new User { Id = 1, Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "admin" },
                new User { Id = 2, Username = "john.doe", Password = BCrypt.Net.BCrypt.HashPassword("Welcome123!"), Role = "user" },
                new User { Id = 3, Username = "emma.smith", Password = BCrypt.Net.BCrypt.HashPassword("Spring2024!"), Role = "user" },
                new User { Id = 4, Username = "liam.jansen", Password = BCrypt.Net.BCrypt.HashPassword("LiamSecure1!"), Role = "user" },
                new User { Id = 5, Username = "olivia.bakker", Password = BCrypt.Net.BCrypt.HashPassword("OliviaPass!23"), Role = "user" },
                new User { Id = 6, Username = "noah.visser", Password = BCrypt.Net.BCrypt.HashPassword("Noah#2024"), Role = "user" },
                new User { Id = 7, Username = "sophia.devries", Password = BCrypt.Net.BCrypt.HashPassword("SophiaSafe!"), Role = "user" },
                new User { Id = 8, Username = "lucas.meijer", Password = BCrypt.Net.BCrypt.HashPassword("Lucas123!"), Role = "user" },
                new User { Id = 9, Username = "mia.mulder", Password = BCrypt.Net.BCrypt.HashPassword("MiaSecure!"), Role = "user" },
                new User { Id = 10, Username = "daan.smit", Password = BCrypt.Net.BCrypt.HashPassword("DaanPass!9"), Role = "user" },
                new User { Id = 11, Username = "zoe.kok", Password = BCrypt.Net.BCrypt.HashPassword("ZoeStrong!"), Role = "user" }
            );
        }
    }
}
