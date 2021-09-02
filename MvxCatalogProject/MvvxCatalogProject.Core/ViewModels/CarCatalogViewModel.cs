using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvxCatalogProject.Core.Models;
using MvvxCatalogProject.Core.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvxCatalogProject.Core.ViewModels
{
    public class CarCatalogViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;
        
        private int _SizeOfPic = 100;

        int lang;

        private ObservableCollection<CarModel> cars = new ObservableCollection<CarModel>();

        private CarModel currentCar = new CarModel();

        public CarCatalogViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        public void InitCommands()
        {


            EngLangCommand = new MvxCommand(() =>
            {
                lang = 0;
            });
            RusLangCommand = new MvxCommand(() =>
            {
                lang = 1;
            });
            SortByTitleCommand = new MvxCommand(() =>
            {
                List<CarModel> tmpList = Sorting.ByTitle(ReturnCarModelsList());
                Cars.Clear();
                foreach(var car in tmpList)
                {
                    Cars.Add(car);
                    CurrentCar = car;
                }
               
            });
            SortByModelCommand = new MvxCommand(() =>
            {
                List<CarModel> tmpList = Sorting.ByModel(ReturnCarModelsList());
                Cars.Clear();
                foreach (var car in tmpList)
                {
                    Cars.Add(car);
                    CurrentCar = car;
                }
            });
            ShowCarCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new ShowCarViewModel(_navigationService)
                {
                    Car = CurrentCar
                });
            });
            EditCommand = new MvxCommand(() =>
            {
                _navigationService.Navigate(new EditCarViewModel(_navigationService)
                {
                    SelectedCar = CurrentCar
                });
                
            });
            AddNewCommand = new MvxCommand(() =>
            {
                CarModel ThisNewCar = new CarModel();
                _navigationService.Navigate(new EditCarViewModel(_navigationService)
                {
                    SelectedCar = ThisNewCar
                });

                Cars.Add(ThisNewCar);

            });
            RemoveCommand = new MvxCommand(() => Cars.Remove(CurrentCar));
            SearchCommand = new MvxCommand(() =>
            {
                Process.Start($"https://www.google.com/search?q={CurrentCar.Title}+{CurrentCar.Model}");
            });
            LoadCommand = new MvxCommand(() => Deserialise());
        }

        public int SizeOfPic
        {
            get => _SizeOfPic;
            set
            {
                _SizeOfPic = value;
                RaisePropertyChanged(() => SizeOfPic);
            }
        }

        public ObservableCollection<CarModel> Cars
        {
            get { return cars; }
            set { SetProperty(ref cars, value); }
        }

        public CarModel CurrentCar
        {
            get => currentCar;
            set
            {
                currentCar = value;
                RaisePropertyChanged(() => CurrentCar);
            }
        }

        public IMvxCommand EditCommand { get; private set; }
        public IMvxCommand AddNewCommand { get; private set; }
        public IMvxCommand RemoveCommand { get; private set; }
        public IMvxCommand LoadCommand { get; private set; }
        public IMvxCommand SaveCommand { get; private set; }
        public IMvxCommand SearchCommand { get; private set; }
        public IMvxCommand ShowCarCommand { get; private set; }
        public IMvxCommand SortByTitleCommand { get; private set; }
        public IMvxCommand SortByModelCommand { get; private set; }
        public IMvxCommand EngLangCommand { get; private set; }
        public IMvxCommand RusLangCommand { get; private set; }


        ~CarCatalogViewModel()
        {
            string photoDirectory = GetPathToFolder();
            string[] photos = Directory.GetFiles(photoDirectory);
            foreach(var photo in photos)
            {
                bool isExists = false;
                foreach(var car in cars)
                {
                    if(photo == car.Photo)
                    {
                        isExists = true;
                        break;
                    }
                }

                if(!isExists)
                {
                    File.Delete(photo);
                }
            }
            string json = JsonConvert.SerializeObject(Cars, Formatting.Indented);
            File.WriteAllText($"{photoDirectory}\\carCatalog.json", json);
            File.WriteAllText($"{GetPathToFolder()}\\language.txt", Convert.ToString(lang));
        }

        public string GetPathToFolder()
        {
            string str = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            for (int i = 0; i < 2; i++)
            {
                str = str.Remove(str.LastIndexOf('\\'));
            }
            str = str + "\\Views\\CarPhotos";
            return str;
        }

        public void Deserialise()
        {
            string json = File.ReadAllText($"{GetPathToFolder()}\\carCatalog.json");
            List<CarModel> tmpCars = new List<CarModel>();
            tmpCars = JsonConvert.DeserializeObject<List<CarModel>>(json);
            foreach (var car in tmpCars)
            {
                Cars.Add(car);
            }
        }

        public List<CarModel> ReturnCarModelsList()
        {
            List<CarModel> tmpList = new List<CarModel>();

            foreach(var car in Cars)
            {
                tmpList.Add(car);
            }

            return tmpList;
        }

    }
}
