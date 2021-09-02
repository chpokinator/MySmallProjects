using MvvmCross.ViewModels;
using MvxProject.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvxProject.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<CarCatalogViewModel>();
        }
    }
}
