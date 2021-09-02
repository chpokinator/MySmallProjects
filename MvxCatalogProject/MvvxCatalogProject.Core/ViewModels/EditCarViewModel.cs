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
    public class EditCarViewModel : MvxViewModel
    {
        private CarModel _SelectedCar = new CarModel();

        private readonly IMvxNavigationService _navigationService;
        public EditCarViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        public void InitCommands()
        {
            CloseCommand = new MvxCommand(() => _navigationService.Close(this));
        }

        public CarModel SelectedCar
        {
            get => _SelectedCar;
            set
            {
                _SelectedCar = value;
                RaisePropertyChanged(() => SelectedCar);
            }
        }
        public IMvxCommand CloseCommand { get; private set; }
    }
}
