using PizzaDelivery.Models;
using System;
using System.Linq;

namespace PizzaDelivery.Repository
{
    public interface IPizzaRepository
    {
        void Create(Pizza pizza);
        void Delete(int id);
        Pizza Read(int id);
        IQueryable<Pizza> ReadAll();
        void Update(Pizza pizza);
    }
}
