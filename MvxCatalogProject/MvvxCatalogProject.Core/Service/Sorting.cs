using MvvxCatalogProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvxCatalogProject.Core.Service
{
    public static class Sorting
    {


        public static List<CarModel> ByTitle(List<CarModel> cars)
        {
            for(int i = 0; i<cars.Count; i++)
            {
                for (int j = i; j < cars.Count; j++)
                {
                    if(cars[i].Title.CompareTo(cars[j].Title)>=0)
                    {
                        CarModel tmp = cars[i];
                        cars[i] = cars[j];
                        cars[j] = tmp;
                    }
                }
            }

            return cars;
        }

        public static List<CarModel> ByModel(List<CarModel> cars)
        {
            for(int i = 0; i<cars.Count; i++)
            {
                for(int j=i; j<cars.Count; j++)
                {
                    if(cars[i].Model.CompareTo(cars[j].Model)>=0)
                    {
                        CarModel tmp = cars[i];
                        cars[i] = cars[j];
                        cars[j] = tmp;
                    }
                }
            }

            return cars;

        }
    }
}
