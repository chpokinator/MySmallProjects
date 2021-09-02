using HRProgram.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
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

namespace MvxStarter.Wpf.Views.NextViews
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeeView.xaml
    /// </summary>
    public partial class EditEmployeeView : MvxWpfView
    {
        public EditEmployeeView()
        {
            InitializeComponent();
        }

        private void ChooseButtonClick(object sender, RoutedEventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image files(*.jpg;*.png;)|*.jpg;*.png)";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    ((EditEmployeeViewModel)DataContext).PathToPic = ofd.FileName;
                    ((EditEmployeeViewModel)DataContext).ChoosePicCommand.Execute(this);
                }
            }
            
        }
    }
}
