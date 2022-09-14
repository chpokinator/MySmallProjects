using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BikesShop.Models
{
   
    public class Bike
    {
        [Key]
        public int BikeId { get; set; }
        [Required(ErrorMessage ="Wrong title")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Wrong price")]
        public double Price { get; set; }
        [Required(ErrorMessage ="Wrong wheelsize")]
        public float WheelSize { get; set; }
        public string Info { get; set; } = "invalid";
        [Required(ErrorMessage ="Wrong color")]
        public string Color { get; set; }
        [Required(ErrorMessage ="Wrong material")]
        public string Material { get; set; }
        public string Photo { get; set; }
    }
}
