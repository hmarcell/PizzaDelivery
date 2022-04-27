using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    [Table("pizzas")]
    public class Pizza : ObservableObject
    {
        private int id;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => SetProperty(ref id, value); }


        string type;
        [Required]
        [StringLength(20)]
        public string Type { get => type; set => SetProperty(ref type, value); }

        int price;
        [Required]
        public int Price { get => price; set => SetProperty(ref price, value); }

        List<Order> orders;
        [NotMapped]
        public virtual List<Order> Orders { get => orders; set => SetProperty(ref orders, value); }         //Contains the orders, in which the given pizza appears

        public override string ToString()
        {
            return $"{Id} {Type} {Price}";
        }
    }
}
