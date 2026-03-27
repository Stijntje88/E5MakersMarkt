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
    internal class AppDbContext : DbContext
    {
        
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin", Status = "Verified" },
                new User { Id = 2, Username = "john.doe", Password = BCrypt.Net.BCrypt.HashPassword("Welcome123!"), Role = "User", Status = "Pending" },
                new User { Id = 3, Username = "emma.smith", Password = BCrypt.Net.BCrypt.HashPassword("Spring2024!"), Role = "User", Status = "Pending" },
                new User { Id = 4, Username = "liam.jansen", Password = BCrypt.Net.BCrypt.HashPassword("LiamSecure1!"), Role = "User", Status = "Pending" },
                new User { Id = 5, Username = "olivia.bakker", Password = BCrypt.Net.BCrypt.HashPassword("OliviaPass!23"), Role = "User", Status = "Pending" },
                new User { Id = 6, Username = "noah.visser", Password = BCrypt.Net.BCrypt.HashPassword("Noah#2024"), Role = "User", Status = "Pending" },
                new User { Id = 7, Username = "sophia.devries", Password = BCrypt.Net.BCrypt.HashPassword("SophiaSafe!"), Role = "User", Status = "Pending" },
                new User { Id = 8, Username = "lucas.meijer", Password = BCrypt.Net.BCrypt.HashPassword("Lucas123!"), Role = "User", Status = "Pending" },
                new User { Id = 9, Username = "mia.mulder", Password = BCrypt.Net.BCrypt.HashPassword("MiaSecure!"), Role = "User", Status = "Pending" },
                new User { Id = 10, Username = "daan.smit", Password = BCrypt.Net.BCrypt.HashPassword("DaanPass!9"), Role = "User", Status = "Pending" },
                new User { Id = 11, Username = "zoe.kok", Password = BCrypt.Net.BCrypt.HashPassword("ZoeStrong!"), Role = "User", Status = "Pending" }
            );

            // Products (uitgebreid)
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Handgemaakte Kaars", Description = "Lavendel geur.", Type = "Decoratie", Img = "kaars.jpg", Material = "Bijenwas", ProductionTime = "2 uur" },
                new Product { Id = 2, Name = "Houten Snijplank", Description = "Eikenhout.", Type = "Keuken", Img = "snijplank.jpg", Material = "Eikenhout", ProductionTime = "5 uur" },
                new Product { Id = 3, Name = "Gehaakte Knuffel", Description = "Voor kinderen.", Type = "Speelgoed", Img = "knuffel.jpg", Material = "Katoen", ProductionTime = "6 uur" },
                new Product { Id = 4, Name = "Keramieken Mok", Description = "Met glazuur.", Type = "Servies", Img = "mok.jpg", Material = "Klei", ProductionTime = "8 uur" },
                new Product { Id = 5, Name = "Leren Portemonnee", Description = "Compact.", Type = "Accessoire", Img = "portemonnee.jpg", Material = "Leer", ProductionTime = "4 uur" },

                new Product { Id = 6, Name = "Macramé Wandhanger", Description = "Boho stijl.", Type = "Decoratie", Img = "macrame.jpg", Material = "Katoen", ProductionTime = "3 uur" },
                new Product { Id = 7, Name = "Houten Lepelset", Description = "Handgesneden.", Type = "Keuken", Img = "lepels.jpg", Material = "Beukenhout", ProductionTime = "2 uur" },
                new Product { Id = 8, Name = "Gegraveerde Sleutelhanger", Description = "Persoonlijk cadeau.", Type = "Accessoire", Img = "sleutelhanger.jpg", Material = "Metaal", ProductionTime = "1 uur" }
            );

            // UserProduct (veel realistischer verdeeld)
            modelBuilder.Entity<UserProduct>().HasData(
                new UserProduct { Id = 1, UserId = 2, ProductId = 1, Datum = new DateTime(2024, 3, 1), Price = 19.99f,Reported = false },
                new UserProduct { Id = 2, UserId = 2, ProductId = 3, Datum = new DateTime(2024, 3, 5), Price = 24.50f, Reported = false },

                new UserProduct { Id = 3, UserId = 3, ProductId = 2, Datum = new DateTime(2024, 3, 2), Price = 34.99f, Reported = false },
                new UserProduct { Id = 4, UserId = 3, ProductId = 6, Datum = new DateTime(2024, 3, 6), Price = 22.00f, Reported = false },

                new UserProduct { Id = 5, UserId = 4, ProductId = 4, Datum = new DateTime(2024, 3, 3), Price = 29.95f, Reported = false },

                new UserProduct { Id = 6, UserId = 5, ProductId = 5, Datum = new DateTime(2024, 3, 4), Price = 39.99f, Reported = false },
                new UserProduct { Id = 7, UserId = 5, ProductId = 8, Datum = new DateTime(2024, 3, 7), Price = 9.99f, Reported = false },

                new UserProduct { Id = 8, UserId = 6, ProductId = 7, Datum = new DateTime(2024, 3, 8), Price = 14.99f, Reported = false },

                new UserProduct { Id = 9, UserId = 7, ProductId = 1, Datum = new DateTime(2024, 3, 9), Price = 18.50f, Reported = false },
                new UserProduct { Id = 10, UserId = 8, ProductId = 2, Datum = new DateTime(2024, 3, 10), Price = 36.00f, Reported = false },
                new UserProduct { Id = 11, UserId = 9, ProductId = 3, Datum = new DateTime(2024, 3, 11), Price = 25.00f, Reported = false },
                new UserProduct { Id = 12, UserId = 10, ProductId = 4, Datum = new DateTime(2024, 3, 12), Price = 31.99f, Reported = false },
                new UserProduct { Id = 13, UserId = 11, ProductId = 5, Datum = new DateTime(2024, 3, 13), Price = 42.50f, Reported = false }
            );
        }

    }
}
