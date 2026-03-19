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
                new User { Id = 1, Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin123") }
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
