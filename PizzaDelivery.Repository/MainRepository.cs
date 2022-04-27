using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Repository
{
    public class MainRepository : IMainRepository
    {
        public IAddressRepository AddressRepo { get; set; }
        public IPizzaRepository PizzaRepo { get; set; }
        public ICustomerRepository CustomerRepo { get; set; }
        public ICourierRepository CourierRepo { get; set; }
        public IOrderRepository OrderRepo { get; set; }

        public MainRepository(IAddressRepository addressRepo, IPizzaRepository pizzaRepo
            , ICustomerRepository customerRepo, ICourierRepository courierRepo, IOrderRepository orderRepo)
        {
            AddressRepo = addressRepo;
            PizzaRepo = pizzaRepo;
            CustomerRepo = customerRepo;
            CourierRepo = courierRepo;
            OrderRepo = orderRepo;
        }
    }
}
