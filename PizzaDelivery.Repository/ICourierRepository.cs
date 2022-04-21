using PizzaDelivery.Models;
using System;
using System.Linq;

namespace PizzaDelivery.Repository
{
    public interface ICourierRepository
    {
        void Create(Courier courier);
        void Delete(int id);
        Courier Read(int id);
        IQueryable<Courier> ReadAll();
        void Update(Courier courier);

        void UpdateStatus(Courier courier, CourierStatus status);
    }
}
