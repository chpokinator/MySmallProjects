using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Models.ViewModels
{
    public class BikesListViewModel
    {
        public List<Bike> Bikes { get; set; }
        public SelectList Colors { get; set; }
        public SelectList Materials { get; set; }
        public int Pages { get; set; }
        public string SortType { get; set; }
    }
}
