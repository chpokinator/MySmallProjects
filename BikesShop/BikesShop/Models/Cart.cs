using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Bike bike, int count)
        {
            CartLine line =
                lineCollection
                .Where(x => x.Bike.BikeId == bike.BikeId)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Bike = bike,
                    Count = count
                });
            }
            else
            {
                line.Count += count;
            }
        }
        public void RemoveLine(Bike bike)
        {
            lineCollection.RemoveAll(x => x.Bike.BikeId == bike.BikeId);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => Convert.ToInt32(x.Bike.Price) * x.Count);
        }

        public IEnumerable<CartLine> Lines
        {
            get => lineCollection;
            set
            {
                lineCollection = (List<CartLine>)value;
            }
        }
    }

    public class CartLine
    {
        public Bike Bike { get; set; }
        public int Count { get; set; }
    }
}
