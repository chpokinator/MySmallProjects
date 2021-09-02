using MvvmCross.ViewModels;
using MvvxCatalogProject.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvxCatalogProject.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            this.RegisterAppStart<CarCatalogViewModel>();
        }
    }
}
