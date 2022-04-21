using PizzaDelivery.Data;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Repository
{
    public class CourierRepository : ICourierRepository
    {
        DeliveryDbContext DbContext;

        public CourierRepository(DeliveryDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public void Create(Courier courier)
        {
            DbContext.Couriers.Add(courier);
            DbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Courier Read(int id)
        {
            return DbContext.Couriers.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Courier> ReadAll()
        {
            return DbContext.Couriers;
        }

        public void Update(Courier courier)
        {
            var oldCourier = Read(courier.Id);
            oldCourier.Status = courier.Status;
            oldCourier.OrderId = courier.OrderId;
            oldCourier.Name = courier.Name;
        }

        public void UpdateStatus(Courier courier, CourierStatus status)
        {
            courier.Status = status;
        }
    }
}
