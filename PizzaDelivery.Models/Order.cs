using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public enum OrderStatus { Cooking, EnRoute }
    [Table("orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Courier))]
        public int CourierId { get; set; }

        [ForeignKey(nameof(Pizza))]
        public int PizzaId { get; set; }
        
        [NotMapped]
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public virtual Courier Courier { get; set; }

        [NotMapped]
        public virtual Pizza Pizza { get; set; }
    }
}
