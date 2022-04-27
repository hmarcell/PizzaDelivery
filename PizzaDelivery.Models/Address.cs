using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    [Table("addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Required]
        public float Distance { get; set; }      // distance from the restaurant, used for calculating the travel time of the couriers

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [NotMapped]
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [NotMapped]
        public virtual Order Order { get; set; }

        public override string ToString()
        {
            return $"{Street} ({Distance}km)";
        }
    }
}
