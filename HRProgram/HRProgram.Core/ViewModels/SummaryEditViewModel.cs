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
    public class SummaryEditViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public SummaryDTO Summary { get; set; } = new SummaryDTO();

        public Action<bool, SummaryDTO> action { get; set; }

        private PositionDTO _SelectedPosition = new PositionDTO();

        public PositionDTO SelectedPosition
        {
            get => _SelectedPosition;
            set
            {
                _SelectedPosition = value;
                RaisePropertyChanged(() => SelectedPosition);
            }
        }

        private MvxObservableCollection<PositionDTO> _Positions = new MvxObservableCollection<PositionDTO>();

        public MvxObservableCollection<PositionDTO> Positions
        {
            get => _Positions;
            set
            {
                SetProperty(ref _Positions, value);
            }
        }

        public SummaryEditViewModel(IMvxNavigationService navigationService, MvxObservableCollection<PositionDTO> positions, Action<bool, SummaryDTO> action)
        {
            this.navigationService = navigationService;
            Positions = positions;
            this.action = action;
            InitCommands();
        }

        public void InitCommands()
        {
            ReturnCommand = new MvxCommand(() =>
            {
                Task.Run(() =>
                {
                    action(false, Summary);
                });
                navigationService.Close(this);
            });
            AcceptCommand = new MvxCommand(() =>
            {
                Task.Run(() =>
                {
                    Summary.Position = SelectedPosition.PostitionName;
                    action(true, Summary);
                });
                navigationService.Close(this);
            });
        }

        public IMvxCommand ReturnCommand { get; private set; }
        public IMvxCommand AcceptCommand { get; private set; }
    }
}
