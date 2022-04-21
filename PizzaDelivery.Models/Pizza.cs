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
    public class Pizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        public int Price { get; set; }

        [NotMapped]
        public virtual List<Order> Orders { get; set; }         //Contains the orders, in which the given pizza appears

        public override string ToString()
        {
            return $"{Id} {Type} {Price}";
        }
    }
}
