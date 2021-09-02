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
    public class EditPositionViewModel : MvxViewModel<PositionDTO, PositionDTO>
    {
        private readonly IMvxNavigationService _navigationService;
        private PositionDTO _parameter;

        public PositionDTO MyObject
        {
            get => _parameter;
            set
            {
                _parameter = value;
                RaisePropertyChanged(() => MyObject);
            }
        }

        public EditPositionViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        public override void Prepare(PositionDTO parameter)
        {
            MyObject = parameter;
        }

        public void InitCommands()
        {
            AcceptCommand = new MvxCommand(async () =>
            {
                await _navigationService.Close(this, MyObject); 
            });

            DeclineCommand = new MvxCommand(async () =>
            {
                await _navigationService.Close(this, new PositionDTO());
            });
        }

        public IMvxCommand AcceptCommand { get; private set; }
        public IMvxCommand DeclineCommand { get; private set; }

    }
}
