using PizzaDelivery.Data;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        DeliveryDbContext DbContext;

        public CustomerRepository(DeliveryDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Customer customer)
        {
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Customer Read(int id)
        {
            return DbContext.Customers.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Customer> ReadAll()
        {
            return DbContext.Customers;
        }

        public void Update(Customer customer)
        {
            var oldCustomer = Read(customer.Id);
            oldCustomer.PhoneNumber = customer.PhoneNumber;
            oldCustomer.Addresses = customer.Addresses;
            oldCustomer.Name = customer.Name;
            oldCustomer.OrderId = customer.OrderId;

        }
    }
}
