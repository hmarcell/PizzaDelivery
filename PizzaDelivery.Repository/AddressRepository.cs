using PizzaDelivery.Data;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Repository
{
    public class AddressRepository : IAddressRepository
    {
        DeliveryDbContext DbContext;

        public AddressRepository(DeliveryDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public void Create(Address address)
        {
            DbContext.Addresses.Add(address);
            DbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Address Read(int id)
        {
            return DbContext.Addresses.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Address> ReadAll()
        {
            return DbContext.Addresses;
        }

        public void Update(Address address)
        {
            var oldAddress = Read(address.Id);
            oldAddress.Street = address.Street;
            oldAddress.CustomerId = address.CustomerId;
            oldAddress.Distance = address.Distance;
        }

    }
}
