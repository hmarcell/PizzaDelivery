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
        public string Address { get; set; }
        public int Distance { get; set; }           //Distance from the restaurant

        [Required]
        public int PhoneNumber { get; set; }

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
