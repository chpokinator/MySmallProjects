using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int BikeId { get; set; }
        [Required(ErrorMessage = "invalid")]
        public int Count { get; set; }
        public double TotalSum { get; set; }
        public Bike Bike { get; set; }
        [Required(ErrorMessage = "invalid")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
