using PizzaDelivery.Models;
using System;
using System.Linq;

namespace PizzaDelivery.Repository
{
    public interface IAddressRepository
    {
        void Create(Address address);
        void Delete(int id);
        Address Read(int id);
        IQueryable<Address> ReadAll();
        void Update(Address address);
    }
}
