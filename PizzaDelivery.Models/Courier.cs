using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public enum CourierStatus { Waiting, Delivering, ComingBack}
    [Table("couriers")]
    public class Courier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [NotMapped]
        public virtual Order Order { get; set; }

        public CourierStatus Status { get; set; }

        public override string ToString()
        {
            return $"{Name} [{Status}]";
        }
    }
}
