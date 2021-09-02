using HRProgram.BLL.DTO;
using HRProgram.BLL.Sevices;
using HRProgram.Core.Service;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Drawing;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRProgram.Core.Services;

namespace HRProgram.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {

        string userPic = "userIcon.png";
        SummaryService summaryService;
        EmployeesService employeeService;
        PositionService positionService;
        StatusesService statusService;

        Action<bool, SummaryDTO> doSummaryEdit;
        Action<bool, EmployeesDTO> doEmployeeEdit;
        private readonly IMvxNavigationService _navigationService;


        private MvxObservableCollection<SummaryDTO> summaries = new MvxObservableCollection<SummaryDTO>();
        private MvxObservableCollection<EmployeesDTO> employees = new MvxObservableCollection<EmployeesDTO>();
        private MvxObservableCollection<PositionDTO> positions = new MvxObservableCollection<PositionDTO>();
        private MvxObservableCollection<StatusesDTO> statuses = new MvxObservableCollection<StatusesDTO>();

        private SummaryDTO _SelectedSummary = new SummaryDTO();
        private EmployeesDTO _SelectedEmployee = new EmployeesDTO();
        private PositionDTO _SelectedPosition = new PositionDTO();
        private StatusesDTO _SelectedStatus = new StatusesDTO();

        public MvxObservableCollection<SummaryDTO> Summaries
        {
            get => summaries;
            set
            {
                SetProperty(ref summaries, value);
            }
        }
        public MvxObservableCollection<EmployeesDTO> Employees
        {
            get => employees;
            set
            {
                SetProperty(ref employees, value);
            }
        }
        public MvxObservableCollection<StatusesDTO> Statuses
        {
            get => statuses;
            set
            {
                SetProperty(ref statuses, value);
            }
        }
        public MvxObservableCollection<PositionDTO> Positions
        {
            get => positions;
            set
            {
                SetProperty(ref positions, value);
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
        public EmployeesDTO SelectedEmployee
        {
            get => _SelectedEmployee;
            set
            {
                _SelectedEmployee = value;
                RaisePropertyChanged(() => SelectedEmployee);
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
        public StatusesDTO SelectedStatus
        {
            get => _SelectedStatus;
            set
            {
                _SelectedStatus = value;
                RaisePropertyChanged(() => SelectedStatus);
            }
        }





        public MainViewModel(IMvxNavigationService navigationService, SummaryService summaryService, EmployeesService employeeService, StatusesService statusService, PositionService positionService)
        {
            this.summaryService = summaryService;
            this.employeeService = employeeService;
            this.statusService = statusService;
            this.positionService = positionService;
            _navigationService = navigationService;
            doSummaryEdit = CheckAndEditSummary;
            doEmployeeEdit = CheckAndEditEmployee;
           

            InitCommands();

            Summaries = new MvxObservableCollection<SummaryDTO>((LoadData.LoadSummaries(summaryService)).Result);
            Employees = new MvxObservableCollection<EmployeesDTO>((LoadData.LoadEmployees(employeeService)).Result);
            Positions = new MvxObservableCollection<PositionDTO>((LoadData.LoadPositions(positionService)).Result);
            Statuses = new MvxObservableCollection<StatusesDTO>((LoadData.LoadStatuses(statusService)).Result);

        }


        public void InitCommands()
        {
            ApplyFiltersCommand = new MvxCommand(() =>
            {
                if (SelectedStatus != null)
                {
                    if (SelectedStatus.StatusId > 0)
                    {
                        List<EmployeesDTO> tmp = (FiltersService.FilterByStatus(SelectedStatus.StatusId, Employees)).ToList();

                        Employees.Clear();

                        foreach (var a in tmp)
                        {
                            Employees.Add(a);
                        }
                    }
                }

                if (SelectedPosition != null)
                {
                    if (SelectedPosition.PositionId > 0)
                    {
                        List<EmployeesDTO> tmp = (FiltersService.FilterByPosition(SelectedPosition.PositionId, Employees)).ToList();

                        Employees.Clear();

                        foreach (var a in tmp)
                        {
                            Employees.Add(a);
                        }
                    }
                }



            });
            ResetFiltersCommand = new MvxCommand(() =>
            {
                Employees.Clear();

                foreach (var a in (LoadData.LoadEmployees(employeeService)).Result)
                {
                    Employees.Add(a);
                }

                SelectedPosition = new PositionDTO();
                SelectedStatus = new StatusesDTO();
            });
            DeleteEmployeeCommand = new MvxCommand(() =>
            {
                employeeService.Delete(SelectedEmployee);
                Employees.Remove(SelectedEmployee);
            });
            DeleteSummaryCommand = new MvxCommand(() =>
            {
                Task.Run(() =>
                {
                    summaryService.Delete(SelectedSummary);
                    Summaries.Remove(SelectedSummary);
                });
                
            });
            DeletePositionCommand = new MvxCommand(() =>
            {
                positionService.Delete(SelectedPosition);
                Positions.Remove(SelectedPosition);
            });
            EditPositionCommand = new MvxCommand(async () =>
            {
                var result = await _navigationService.Navigate<EditPositionViewModel, PositionDTO, PositionDTO>(new PositionDTO());

                if (result != null)
                {
                    if (result.PostitionName != null)
                    {
                        SelectedPosition.PostitionName = result.PostitionName;
                        positionService.CreateOrUpdate(SelectedPosition);
                        List<PositionDTO> tmp = (positionService.GetAll().ToList());

                        Positions.Clear();
                        SelectedPosition = null;

                        foreach (var a in tmp)
                        {
                            Positions.Add(a);
                        }
                    }



                }
            });
            AddPositionCommand = new MvxCommand(async () =>
            {
                var result = await _navigationService.Navigate<EditPositionViewModel, PositionDTO, PositionDTO>(new PositionDTO());

                if (result != null)
                {
                    if (result.PostitionName != null)
                    {
                        PositionDTO newPosition = new PositionDTO() { PostitionName = result.PostitionName };
                        positionService.CreateOrUpdate(newPosition);
                        List<PositionDTO> tmp = (positionService.GetAll().ToList());

                        Positions.Clear();
                        SelectedPosition = null;

                        foreach (var a in tmp)
                        {
                            Positions.Add(a);
                        }
                    }

                }
            });
            ViewEmployeeSummaryCommand = new MvxCommand(() =>
            {

                _navigationService.Navigate(new DetailedSummaryViewModel(_navigationService, summaryService.Get(Convert.ToInt32(SelectedEmployee.SummaryId)), SelectedEmployee));



            });
            EditSummaryCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new SummaryEditViewModel(_navigationService, Positions, doSummaryEdit) { Summary = SelectedSummary});
            });
            AddSummaryCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new SummaryEditViewModel(_navigationService, Positions, doSummaryEdit));
            });
            ViewSummaryCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new DetailedSummaryViewModel(_navigationService, SelectedSummary, new EmployeesDTO() { Photo = ImageToArrayConverter.ImgToByte(userPic) }));
            });
            EditEmployeeCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new EditEmployeeViewModel(_navigationService, Positions, Summaries, Statuses, SelectedEmployee, doEmployeeEdit));
            });
            AddEmployeeCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new EditEmployeeViewModel(_navigationService, Positions, Summaries, Statuses, new EmployeesDTO(), doEmployeeEdit));
            });
            DetailedEmployeeView = new MvxCommand(() =>
            {
                _navigationService.Navigate(new EmployeeDetailedViewModel(_navigationService,
                    SelectedEmployee,
                    positionService.Get(Convert.ToInt32(SelectedEmployee.PositionId)),
                    statusService.Get(Convert.ToInt32(SelectedEmployee.StatusId))));
            });

        }

        


        public void CheckAndEditSummary(bool check, SummaryDTO summary)
        {
            if(check)
            {
                summaryService.CreateOrUpdate(summary);

                Summaries.Clear();
                Task.Run(() =>
                {
                    foreach(var a in summaryService.GetAll())
                    {
                        Summaries.Add(a);
                    }
                });
                
            }
        }

        public void CheckAndEditEmployee(bool check, EmployeesDTO employee)
        {
            if (check)
            {
                employeeService.CreateOrUpdate(employee);

                Employees.Clear();
                Task.Run(() =>
                {
                    foreach (var a in employeeService.GetAll())
                    {
                        Employees.Add(a);
                    }
                });

            }
        }

        public IMvxCommand ApplyFiltersCommand { get; private set; }
        public IMvxCommand ResetFiltersCommand { get; private set; }
        public IMvxCommand DeleteEmployeeCommand { get; private set; }
        public IMvxCommand DeleteSummaryCommand { get; private set; }
        public IMvxCommand DeletePositionCommand { get; private set; }
        public IMvxCommand EditPositionCommand { get; private set; }
        public IMvxCommand EditSummaryCommand { get; private set; }
        public IMvxCommand EditEmployeeCommand { get; private set; }
        public IMvxCommand AddPositionCommand { get; private set; }
        public IMvxCommand AddSummaryCommand { get; private set; }
        public IMvxCommand AddEmployeeCommand { get; private set; }
        public IMvxCommand ViewEmployeeSummaryCommand { get; private set; }
        public IMvxCommand ViewSummaryCommand { get; private set; }
        public IMvxCommand DetailedEmployeeView { get; private set; }

    }
}
