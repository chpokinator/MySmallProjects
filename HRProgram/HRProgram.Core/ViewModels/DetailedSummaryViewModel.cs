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
    public class DetailedSummaryViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public SummaryDTO Summary { get; set; }
        public EmployeesDTO Employee { get; set; }

        public DetailedSummaryViewModel(IMvxNavigationService navigationService, SummaryDTO summary, EmployeesDTO employee)
        {
            Summary = summary;
            Employee = employee;
            this.navigationService = navigationService;
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
