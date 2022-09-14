using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Models
{
    public class DefaultBikesData
    {
        public static void Initialize(BikesContext context)
        {
            if (!context.Bikes.Any())
            {
                for(int i =0; i < 5; i++)
                context.Bikes.AddRange(
                    new Bike
                    {
                        Color = "Green",
                        Price = 228,
                        Title = "Bruh",
                        WheelSize = 29,
                        Info = "Info",
                        Material = "steel"
                    },
                     new Bike
                     {
                         Color = "Cyan",
                         Price = 228,
                         Title = "Bruh",
                         WheelSize = 29,
                         Info = "Info",
                         Material = "steel"
                     },
                      new Bike
                      {
                          Color = "Red",
                          Price = 228,
                          Title = "Bruh",
                          WheelSize = 29,
                          Info = "Info",
                          Material = "steel"
                      }


                    );

                context.SaveChanges();
            }

        }
    }
}
