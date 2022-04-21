using PizzaDelivery.Models;
using System;
using System.Linq;

namespace PizzaDelivery.Repository
{
    public interface IOrderRepository
    {
        void Create(Order order);
        void Delete(int id);
        Order Read(int id);
        IQueryable<Order> ReadAll();
        void Update(Order order);
        void UpdateStatus(Order order, OrderStatus status);
    }
}
