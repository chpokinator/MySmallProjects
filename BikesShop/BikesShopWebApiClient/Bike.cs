using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikesShopWebApiClient
{
    public class Bike
    {
        
        public int BikeId { get; set; }
        
        public string Title { get; set; }
        
        public double Price { get; set; }
        
        public float WheelSize { get; set; }
        public string Info { get; set; } = "invalid";
        
        public string Color { get; set; }
        
        public string Material { get; set; }
    }
}
