using PizzaDelivery.Data;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Repository
{
    public class OrderRepository : IOrderRepository
    {
        DeliveryDbContext DbContext;

        public OrderRepository(DeliveryDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Order order)
        {
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Order Read(int id)
        {
            return DbContext.Orders.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Order> ReadAll()
        {
            return DbContext.Orders;
        }

        public void Update(Order order)
        {
            var oldOrder = Read(order.Id);
            oldOrder.CourierId = order.CourierId;
            oldOrder.PizzaId = order.PizzaId;
            oldOrder.CustomerId = order.CustomerId;
            oldOrder.Status = order.Status;
        }
        public void UpdateStatus(Order order, OrderStatus status)
        {
            order.Status = status;
        }
    }
}
