using System;
using System.Linq;

namespace PizzaDelivery.Repository
{
    public interface IMainRepository
    {
        IAddressRepository AddressRepo { get; set; }
        ICourierRepository CourierRepo { get; set; }
        ICustomerRepository CustomerRepo { get; set; }
        IOrderRepository OrderRepo { get; set; }
        IPizzaRepository PizzaRepo { get; set; }
    }
}
