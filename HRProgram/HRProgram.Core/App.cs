using HRProgram.BLL.Sevices;
using HRProgram.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<EmployeesService, EmployeesService>();
            Mvx.IoCProvider.RegisterType<PositionService, PositionService>();
            Mvx.IoCProvider.RegisterType<StatusesService, StatusesService>();
            Mvx.IoCProvider.RegisterType<SummaryService, SummaryService>();

            this.RegisterAppStart<MainViewModel>();
        }
    }
}
