using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [NotMapped]         //remove
        public Dictionary<string, float> Addresses { get; set; }              // float: distance from the restaurant, used for calculating the travel time of the couriers

        public string SelectedAddress { get; set; }        

        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [NotMapped]
        public Order Order { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name} {PhoneNumber}";
        }
    }
}
