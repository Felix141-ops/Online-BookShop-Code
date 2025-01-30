using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_BookShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_BookShop.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        /*Products is the name of the table as it appears in the database*/
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); /*configuring the model(database schema) using the ModelBuilder API*/

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
              new Product
              {
                  ProductId = 1,
                  Title = "Fortune of Time",
                  Author = "Billy Spark",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ISBN = "SWD9999001",
                  ListPrice = 990,
                  Price = 900,
                  Price50 = 850,
                  Price100 = 800,
                  CategoryId = 1,
                  ImageUrl = "" 
              },
              new Product
              {
                  ProductId = 2,
                  Title = "Dark Skies",
                  Author = "Nancy Hoover",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ISBN = "CAW777777701",
                  ListPrice = 400,
                  Price = 300,
                  Price50 = 250,
                  Price100 = 200,
                  CategoryId = 1,
                  ImageUrl = ""
              },
              new Product
              {
                  ProductId = 3,
                  Title = "Vanish in the Sunset",
                  Author = "Julian Button",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ISBN = "RITO5555501",
                  ListPrice = 550,
                  Price = 500,
                  Price50 = 400,
                  Price100 = 350,
                  CategoryId = 2,
                  ImageUrl = ""
              },
              new Product
              {
                  ProductId = 4,
                  Title = "Cotton Candy",
                  Author = "Abby Muscles",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ISBN = "WS3333333301",
                  ListPrice = 700,
                  Price = 650,
                  Price50 = 600,
                  Price100 = 550,
                  CategoryId = 2,
                  ImageUrl = ""
              },
              new Product
              {
                  ProductId = 5,
                  Title = "Rock in the Ocean",
                  Author = "Ron Parker",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ISBN = "SOTJ1111111101",
                  ListPrice = 300,
                  Price = 270,
                  Price50 = 250,
                  Price100 = 200,
                  CategoryId = 3,
                  ImageUrl = ""
              },
              new Product
              {
                  ProductId = 6,
                  Title = "Leaves and Wonders",
                  Author = "Laura Phantom",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ISBN = "FOT000000001",
                  ListPrice = 250,
                  Price = 230,
                  Price50 = 220,
                  Price100 = 200,
                  CategoryId = 1,
                  ImageUrl = ""
              }
              );
        
    }
    }
}
