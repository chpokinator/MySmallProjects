using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvxCatalogProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvxCatalogProject.Core.ViewModels
{
    public class ShowCarViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public ShowCarViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        public void InitCommands()
        {
            CloseCommand = new MvxCommand(() => _navigationService.Close(this));
        }

        public CarModel Car { get; set; }

        public IMvxCommand CloseCommand { get; private set; }
    }
}
