using HRProgram.BLL.DTO;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.Core.ViewModels
{
    public class EmployeeDetailedViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public EmployeesDTO Employee { get; set; }
        public PositionDTO Position { get; set; }
        public StatusesDTO Status { get; set; }

        public EmployeeDetailedViewModel(IMvxNavigationService navigationService, EmployeesDTO employee, PositionDTO position, StatusesDTO status)
        {
            this.navigationService = navigationService;

            Employee = employee;
            Position = position;
            Status = status;

            InitCommands();
        }

        public void InitCommands()
        {
            ReturnCommand = new MvxCommand(() =>
            {
                navigationService.Close(this);
            });
        }

        public IMvxCommand ReturnCommand { get; private set; }
    }
}
