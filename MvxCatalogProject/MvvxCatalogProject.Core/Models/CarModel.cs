using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core;

namespace MvvxCatalogProject.Core.Models
{

    [Serializable]

    public class CarModel
    {
        public string Title { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
