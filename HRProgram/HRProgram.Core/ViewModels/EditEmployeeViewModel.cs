using HRProgram.BLL.DTO;
using HRProgram.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.Core.ViewModels
{
    public class EditEmployeeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        private MvxObservableCollection<PositionDTO> _Positions = new MvxObservableCollection<PositionDTO>();
        private MvxObservableCollection<SummaryDTO> _Summaries = new MvxObservableCollection<SummaryDTO>();
        private MvxObservableCollection<StatusesDTO> _Statuses = new MvxObservableCollection<StatusesDTO>();

        private PositionDTO _SelectedPosition = new PositionDTO();
        private SummaryDTO _SelectedSummary = new SummaryDTO();
        private StatusesDTO _SelectedStatus = new StatusesDTO();

        public EmployeesDTO Employee { get; set; }

        private int _MaxValueSalary = 100000;
        private int _MaxValuePremium = 100000;
        private int _SalaryStep = 100;
        private int _PremiumStep = 100;

        public int MaxValueSalary { get =>_MaxValueSalary; set { _MaxValueSalary = value; RaisePropertyChanged(() => MaxValueSalary); } }
        public int MaxValuePremium { get =>_MaxValuePremium; set { _MaxValuePremium = value; RaisePropertyChanged(() => MaxValuePremium); } }
        public int SalaryStep { get=>_SalaryStep; set { _SalaryStep = value; RaisePropertyChanged(() => SalaryStep); } }
        public int PremiumStep { get=>_PremiumStep; set { _PremiumStep = value; RaisePropertyChanged(() => PremiumStep); } }
        public string PathToPic { get; set; }

        public MvxObservableCollection<PositionDTO> Positions
        {
            get => _Positions;
            set
            {
                SetProperty(ref _Positions, value);
            }
        }

        public MvxObservableCollection<SummaryDTO> Summaries
        {
            get => _Summaries;
            set
            {
                SetProperty(ref _Summaries, value);
            }
        }

        public MvxObservableCollection<StatusesDTO> Statuses
        {
            get => _Statuses;
            set
            {
                SetProperty(ref _Statuses, value);
            }
        }

        public PositionDTO SelectedPosition
        {
            get => _SelectedPosition;
            set
            {
                _SelectedPosition = value;
                RaisePropertyChanged(() => SelectedPosition);
            }
        }

        public SummaryDTO SelectedSummary
        {
            get => _SelectedSummary;
            set
            {
                _SelectedSummary = value;
                RaisePropertyChanged(() => SelectedSummary);
            }
        }

        public StatusesDTO SelectedStatus
        {
            get => _SelectedStatus;
            set
            {
                _SelectedStatus = value;
                RaisePropertyChanged(() => SelectedStatus);
            }
        }

        Action<bool, EmployeesDTO> action;

        public EditEmployeeViewModel(IMvxNavigationService navigationService,
            MvxObservableCollection<PositionDTO> positions,
            MvxObservableCollection<SummaryDTO> summaries,
            MvxObservableCollection<StatusesDTO> statuses,
            EmployeesDTO employee,
            Action<bool, EmployeesDTO> action)
        {
            Employee = employee;
            this.navigationService = navigationService;
            Positions = positions;
            Summaries = summaries;
            Statuses = statuses;
            this.action = action;
            InitCommands();

        }

        public void InitCommands()
        {
            ReturnCommand = new MvxCommand(() =>
            {
                Task.Run(() =>
                {
                    action(false, Employee);
                });
                navigationService.Close(this);
            });
            AcceptCommand = new MvxCommand(() =>
            {
                Task.Run(() =>
                {
                    Employee.SummaryId = SelectedSummary.SummaryId;
                    Employee.PositionId = SelectedPosition.PositionId;
                    Employee.StatusId = SelectedStatus.StatusId;
                    action(true, Employee);
                });
                navigationService.Close(this);
            });
            ChoosePicCommand = new MvxCommand(() =>
            {
                Task.Run(() =>
                {
                    Employee.Photo = ImageToArrayConverter.ImgToByte(PathToPic);
                });
            });
        }

        public IMvxCommand ReturnCommand { get; private set; }
        public IMvxCommand AcceptCommand { get; private set; }
        public IMvxCommand ChoosePicCommand { get; private set; }
    }
}
