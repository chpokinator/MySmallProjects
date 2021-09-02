using MvvmCross.Platforms.Wpf.Views;
using System;
using System.IO;
using System.Windows;

namespace MvxStarter.Wpf.Views
{
    /// <summary>
    /// Логика взаимодействия для CarCatalogView.xaml
    /// </summary>
    public partial class CarCatalogView : MvxWpfView
    {
        public CarCatalogView()
        {
            InitializeComponent();

            Uri uri = new Uri("Themes/LightTheme.xaml", UriKind.Relative);

            ResourceDictionary dictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);



        }

        private void Dark_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);

            ResourceDictionary dictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        private void Light_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("Themes/LightTheme.xaml", UriKind.Relative);

            ResourceDictionary dictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        private void enLan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Restart to change language", "info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ruLan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Перезапустите чтобы применить изменения", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
