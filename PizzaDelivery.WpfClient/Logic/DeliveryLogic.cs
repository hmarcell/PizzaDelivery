using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.WpfClient.Logic
{
    public class DeliveryLogic
    {
        IOrderTakingService orderTakingService;
        public DeliveryLogic(IOrderTakingService orderTakingService)
        {
            this.orderTakingService = orderTakingService;
        }
    }
}
