using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Views;
using System.IO;

namespace MvxStarter.Wpf.Views.NextViews
{
    /// <summary>
    /// Логика взаимодействия для EditCarView.xaml
    /// </summary>
    public partial class EditCarView : MvxWpfView
    {
        public EditCarView()
        {
            InitializeComponent();
        }

        private void ChooseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string path = GetPathToFolder();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                
                string fileName = openFileDialog.FileName;
                fileName = fileName.Remove(0, fileName.LastIndexOf('\\'));
                File.Copy(openFileDialog.FileName, path + fileName, true);
                link.Text = path + fileName;

            }
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
    }
}
