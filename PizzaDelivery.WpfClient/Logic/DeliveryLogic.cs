using PizzaDelivery.Models;
using PizzaDelivery.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.WpfClient.Logic
{
    public class DeliveryLogic
    {
        IOrderTakingService orderTakingService;
        IMainRepository Repository;
        ObservableCollection<Courier> Couriers { get; set; }
        public DeliveryLogic(IOrderTakingService orderTakingService)
        {
            this.orderTakingService = orderTakingService;
            Couriers = new ObservableCollection<Courier>(Repository.CourierRepo.ReadAll());
        }
    }
}
