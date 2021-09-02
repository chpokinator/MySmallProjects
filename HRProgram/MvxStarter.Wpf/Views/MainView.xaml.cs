using HRProgram.BLL.DTO;
using HRProgram.Core.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Views;
using MvxStarter.Wpf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvxStarter.Wpf.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : MvxWpfView
    {
        public MainView()
        {
            InitializeComponent();
        }

       

        private void PositionEditButtonClicked(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)PositionsListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            PositionsListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).EditPositionCommand.Execute(this);
        }

        private void PositionDeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)PositionsListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            PositionsListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).DeletePositionCommand.Execute(this);
        }

        private void ViewSummaryEmployeeClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)EmployeesListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            EmployeesListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).ViewEmployeeSummaryCommand.Execute(this);
        }


        private void EditEmployeeClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)EmployeesListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            EmployeesListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).EditEmployeeCommand.Execute(this);
        }

        private void DetailedEmployeeClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)EmployeesListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            EmployeesListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).DetailedEmployeeView.Execute(this);
        }

        private void DeleteEmployeeClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)EmployeesListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            EmployeesListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).DeleteEmployeeCommand.Execute(this);
        }

        private void SummaryEditClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)SummaryListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            SummaryListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).EditSummaryCommand.Execute(this);
        }

        private void SummaryDeleteClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)SummaryListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            SummaryListView.SelectedItem = curItem;

            ((MainViewModel)DataContext).DeleteSummaryCommand.Execute(this);
        }

        private void ExportSummaryClick(object sender, RoutedEventArgs e)
        {
            var curItem = ((System.Windows.Controls.ListViewItem)SummaryListView.ContainerFromElement((System.Windows.Controls.Button)sender)).Content;

            SummaryListView.SelectedItem = curItem;

            SummaryDTO summary = curItem as SummaryDTO;

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "Text files(*.txt)|*.txt";

            if(sfd.ShowDialog()==DialogResult.OK)
            {
                ExportSummaryService.ExportSummary(summary, sfd.FileName);
            }
        }
    }
}
