using PizzaDelivery.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.WpfClient.Logic
{
    public class OrderTakingViaWindow : IOrderTakingService
    {
        public void OpenWindow()
        {
            new OrderTakingWindow().ShowDialog();
        }
    }
}
