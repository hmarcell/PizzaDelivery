using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;

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

            DbSeed(modelBuilder);

        }

        private void DbSeed(ModelBuilder modelBuilder)
        {
            Pizza pizzaMargherita = new Pizza() { Type = "Margherita", Price = 10 };
            Pizza pizzaHawaii = new Pizza() { Type = "Hawaii", Price = 14 };
            Pizza pizzaPepperoni = new Pizza() { Type = "Pepperoni", Price = 14 };
            Pizza pizzaSupreme = new Pizza() { Type = "Supreme", Price = 20 };

            Courier courierCarl = new Courier() { Name = "Courier Carl" };
            Courier courierCharlie = new Courier() { Name = "Courier Charlie" };
            Courier courierChloe = new Courier() { Name = "Courier Chloe" };

            Customer customer1 = new Customer() { Name = "John Doe", Addresses = new Dictionary<string, float>(), PhoneNumber = "703-501-2417" };               //Data generated with FakeNameGenerator
            customer1.Addresses.Add("Neighbor Street 1.", 0.5f);
            Customer customer2 = new Customer() { Name = "Joann Carter", Addresses = new Dictionary<string, float>(), PhoneNumber = "703-786-3082" };
            customer2.Addresses.Add("Perine Street 10.", 4.2f);
            Customer customer3 = new Customer() { Name = "Daniel Samora", Addresses = new Dictionary<string, float>(), PhoneNumber = "931-787-9264" };
            customer3.Addresses.Add("Glory Road 13.", 2.4f);
            Customer customer4 = new Customer() { Name = "Scott Washington", Addresses = new Dictionary<string, float>(), PhoneNumber = "415-377-7029" };
            customer4.Addresses.Add("Roosevelt Street 2.", 1.2f);
            Customer customer5 = new Customer() { Name = "Roy Peck", Addresses = new Dictionary<string, float>(), PhoneNumber = "903-783-9992" };
            customer5.Addresses.Add("Hall Place 211.", 3.6f);
            Customer customer6 = new Customer() { Name = "Donald Gaddy", Addresses = new Dictionary<string, float>(), PhoneNumber = "505-331-8194" };
            customer6.Addresses.Add("Faraway Street 738.", 8.2f);
            Customer customer7 = new Customer() { Name = "Keith Wolf", Addresses = new Dictionary<string, float>(), PhoneNumber = "406-630-9883" };
            customer7.Addresses.Add("Reel Avenue 5.", 5.8f);            



            modelBuilder.Entity<Pizza>().HasData(pizzaMargherita, pizzaHawaii, pizzaPepperoni, pizzaSupreme);
            modelBuilder.Entity<Courier>().HasData(courierCarl, courierCharlie, courierChloe);
            modelBuilder.Entity<Customer>().HasData(customer1, customer2, customer3, customer4, customer5, customer6, customer7);

        }
    }
}
