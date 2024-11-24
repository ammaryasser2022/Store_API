using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Entities.OrderEntities;

namespace Store.Repository.Data.Contexts
{
    public class StoreDbContext : DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)  
        {
            
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }




        // i will not make OnConfiguring --> the connection string will be on Main 

        public DbSet<Product>  Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductType>  Types { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }





    }
}
