using PizzaDelivery.Models;
using System;
using System.Linq;

namespace PizzaDelivery.Repository
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        void Delete(int id);
        Customer Read(int id);
        IQueryable<Customer> ReadAll();
        void Update(Customer customer);
    }
}
