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
        public virtual DbSet<Address> Addresses { get; set; }
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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()                        //order-customer
                .HasOne(o => o.Customer)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()                      //address-customer
                .HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            DbSeed(modelBuilder);

        }

        private void DbSeed(ModelBuilder modelBuilder)
        {
            Pizza pizzaMargherita = new Pizza() { Id = 4 ,Type = "Margherita", Price = 10 };
            Pizza pizzaHawaii = new Pizza() { Id = 1, Type = "Hawaii", Price = 14 };
            Pizza pizzaPepperoni = new Pizza() { Id = 2, Type = "Pepperoni", Price = 14 };
            Pizza pizzaSupreme = new Pizza() { Id = 3, Type = "Supreme", Price = 20 };

            Courier courierCarl = new Courier() { Id = 3, Name = "Courier Carl"};
            Courier courierCharlie = new Courier() { Id = 1, Name = "Courier Charlie" };
            Courier courierChloe = new Courier() { Id = 2, Name = "Courier Chloe" };

            Customer customer1 = new Customer() { Id = 7, Name = "John Doe", PhoneNumber = "703-501-2417" };               //Data generated with FakeNameGenerator
            Customer customer2 = new Customer() { Id = 1, Name = "Joann Carter", PhoneNumber = "703-786-3082" };
            Customer customer3 = new Customer() { Id = 2, Name = "Daniel Samora", PhoneNumber = "931-787-9264" };
            Customer customer4 = new Customer() { Id = 3, Name = "Scott Washington", PhoneNumber = "415-377-7029" };
            Customer customer5 = new Customer() { Id = 4, Name = "Roy Peck", PhoneNumber = "903-783-9992" };
            Customer customer6 = new Customer() { Id = 5, Name = "Donald Gaddy", PhoneNumber = "505-331-8194" };
            Customer customer7 = new Customer() { Id = 6, Name = "Keith Wolf", PhoneNumber = "406-630-9883" };

            Address address1 = new Address() { Id = 1, Street = "Perine Street 10.", Distance = 4.2f, CustomerId = 1 };
            Address address2 = new Address() { Id = 2, Street = "Glory Road 13.", Distance = 2.4f, CustomerId = 2 };
            Address address3 = new Address() { Id = 3, Street = "Roosevelt Street 2.", Distance = 1.2f, CustomerId = 3 };
            Address address4 = new Address() { Id = 4, Street = "Hall Place 211.", Distance = 3.6f, CustomerId = 4 };
            Address address5 = new Address() { Id = 5, Street = "Faraway Street 738.", Distance = 8.2f, CustomerId = 5 };
            Address address6 = new Address() { Id = 6, Street = "Reel Avenue 5.", Distance = 5.8f, CustomerId = 6 };
            Address address7 = new Address() { Id = 7, Street = "Neighbor Street 1.", Distance = 0.5f, CustomerId = 7 };
            Address address8 = new Address() { Id = 8, Street = "Neighbor Street 15.", Distance = 0.6f, CustomerId = 1 };
            Address address9 = new Address() { Id = 9, Street = "Faraway Street 240", Distance = 6.5f, CustomerId = 2 };


            modelBuilder.Entity<Pizza>().HasData(pizzaMargherita, pizzaHawaii, pizzaPepperoni, pizzaSupreme);
            modelBuilder.Entity<Courier>().HasData(courierCarl, courierCharlie, courierChloe);
            modelBuilder.Entity<Customer>().HasData(customer1, customer2, customer3, customer4, customer5, customer6, customer7);
            modelBuilder.Entity<Address>().HasData(address1, address2, address3, address4, address5, address6, address7, address8, address9);

        }
    }
}
