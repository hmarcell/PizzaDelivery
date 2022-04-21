using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;

namespace PizzaDelivery.Data
{
    //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Db.mdf;Integrated Security=True
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
        {
        }

        public DeliveryDbContext()
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }      
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Db.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()                        //order-pizza
                .HasOne(o => o.Pizza)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PizzaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()                        //order-courier
                .HasOne(o => o.Courier)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CourierId)
                .HasForeignKey<Courier>(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()                        //order-customer
                .HasOne(o => o.Customer)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CustomerId)
                .HasForeignKey<Customer>(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
