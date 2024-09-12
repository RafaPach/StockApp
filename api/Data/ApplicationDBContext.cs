using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) 
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Porfolio> Porfolios {get; set;}

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder)

            // Porfolio OnModelCreating

            //The Entity Framework Core Fluent API HasKey method is used to denote the property that uniquely identifies an entity (the EntityKey), and which is mapped to the Primary Key field in a databas

            builder.Entity<Portofolio>(x=>x.HasKey(p=> new {p.AppUserId, p.StockId}))


            // Entity<TEntity>()  - Returns an object that can be used to configure a given entity type in the model. If the entity type is not already part of the model, it will be added to the model.

            // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many 
            // Good source to understand the below

             builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            
                builder.Entity<Portfolio>()
                .HasMany(u => u.Porfolio)
                .WithOne(u => u.Stock)
                .HasForeignKey(p => p.StockId);

            // Roles on ModelCreating

            List<IdentityRole> roles = new List<IdentityRole>

            new IdentityRole  {
                Name = "Admin",
                NormalizedName = "ADMiN"

                // Normalized Name it only means that it is capitalized
            }, 
                    new IdentityRole {
                        Name = "User",
                        NormalizedName = "USER"
                    }
        }

        builder.Entity<IdentityRole>().HasData(roles);

        // HasData adds seed data to this entity type. It is used to generate data motion migrations.

    }
}