using MvvmCross.Platforms.Wpf.Views;
using System;
using System.IO;

namespace MvxStarter.Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MvxWindow
    {
        public MainWindow()
        {
            int lang = 0;

            if(File.Exists($"{GetPathToFolder()}\\language.txt"))
            {
                lang = Convert.ToInt32(File.ReadAllText($"{GetPathToFolder()}\\language.txt"));
            }

            switch (lang)
            {
                case 0:
                    {
                        ChangeLocalization("en-US");
                        break;
                    }
                case 1:
                    {
                        ChangeLocalization("ru-RU");
                        break;
                    }
            }

            InitializeComponent();
        }

        private string GetPathToFolder()
        {
            string str = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            for (int i = 0; i < 2; i++)
            {
                str = str.Remove(str.LastIndexOf('\\'));
            }
            str = str + "\\Views\\CarPhotos";
            return str;
        }

        private void ChangeLocalization(string str)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                            System.Globalization.CultureInfo.GetCultureInfo(str);
        }
    }
}
