﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistance.Common;

namespace ProniaOnion.Persistance.Contexts
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyQueryFilters();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entites = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in entites)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.ModifiedAt = DateTime.Now;
                        break;

                    
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
