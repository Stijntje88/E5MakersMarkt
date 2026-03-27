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
                new User { Id = 1, Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin", Satus = "Verified" },
                new User { Id = 2, Username = "john.doe", Password = BCrypt.Net.BCrypt.HashPassword("Welcome123!"), Role = "User", Satus = "Pending" },
                new User { Id = 3, Username = "emma.smith", Password = BCrypt.Net.BCrypt.HashPassword("Spring2024!"), Role = "User", Satus = "Pending" },
                new User { Id = 4, Username = "liam.jansen", Password = BCrypt.Net.BCrypt.HashPassword("LiamSecure1!"), Role = "User", Satus = "Pending" },
                new User { Id = 5, Username = "olivia.bakker", Password = BCrypt.Net.BCrypt.HashPassword("OliviaPass!23"), Role = "User", Satus = "Pending" },
                new User { Id = 6, Username = "noah.visser", Password = BCrypt.Net.BCrypt.HashPassword("Noah#2024"), Role = "User", Satus = "Pending" },
                new User { Id = 7, Username = "sophia.devries", Password = BCrypt.Net.BCrypt.HashPassword("SophiaSafe!"), Role = "User", Satus = "Pending" },
                new User { Id = 8, Username = "lucas.meijer", Password = BCrypt.Net.BCrypt.HashPassword("Lucas123!"), Role = "User", Satus = "Pending" },
                new User { Id = 9, Username = "mia.mulder", Password = BCrypt.Net.BCrypt.HashPassword("MiaSecure!"), Role = "User", Satus = "Pending" },
                new User { Id = 10, Username = "daan.smit", Password = BCrypt.Net.BCrypt.HashPassword("DaanPass!9"), Role = "User", Satus = "Pending" },
                new User { Id = 11, Username = "zoe.kok", Password = BCrypt.Net.BCrypt.HashPassword("ZoeStrong!"), Role = "User", Satus = "Pending" }
            );
            modelBuilder.Entity<Product>().HasData(
     new Product
     {
         Id = 1,
         Name = "Handgemaakte Kaars",
         Description = "Een sfeervolle handgemaakte kaars met lavendel geur.",
         Type = "Decoratie",
         Img = "kaars.jpg",
         Material = "Bijenwas",
         ProductionTime = "2 uur"
     },
     new Product
     {
         Id = 2,
         Name = "Houten Snijplank",
         Description = "Robuuste snijplank van eikenhout, perfect voor in de keuken.",
         Type = "Keuken",
         Img = "snijplank.jpg",
         Material = "Eikenhout",
         ProductionTime = "5 uur"
     },
     new Product
     {
         Id = 3,
         Name = "Gehaakte Knuffel",
         Description = "Schattige gehaakte knuffel, leuk voor kinderen.",
         Type = "Speelgoed",
         Img = "knuffel.jpg",
         Material = "Katoen",
         ProductionTime = "6 uur"
     },
     new Product
     {
         Id = 4,
         Name = "Keramieken Mok",
         Description = "Unieke handgemaakte mok met glazuur afwerking.",
         Type = "Servies",
         Img = "mok.jpg",
         Material = "Klei",
         ProductionTime = "8 uur"
     },
     new Product
     {
         Id = 5,
         Name = "Leren Portemonnee",
         Description = "Compacte portemonnee van echt leer.",
         Type = "Accessoire",
         Img = "portemonnee.jpg",
         Material = "Leer",
         ProductionTime = "4 uur"
     }
 );
        }

    }
}
