using PizzaDelivery.Data;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Repository
{
    public class PizzaRepository : IPizzaRepository
    {
        DeliveryDbContext DbContext;

        public PizzaRepository(DeliveryDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void Create(Pizza pizza)
        {
            DbContext.Pizzas.Add(pizza);
            DbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Pizza Read(int id)
        {
            return DbContext.Pizzas.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Pizza> ReadAll()
        {
            return DbContext.Pizzas;
        }

        public void Update(Pizza pizza)
        {
            var oldPizza = Read(pizza.Id);
            oldPizza.Price = pizza.Price;
            oldPizza.Type = pizza.Type;
        }
    }
}
