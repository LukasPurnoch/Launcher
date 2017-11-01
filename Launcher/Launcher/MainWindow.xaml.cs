using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileHelpers;
using System.IO;

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> slnPath = new List<string>();
        List<string> slnName = new List<string>();
        //List<DirPath> pathsList = new List<DirPath>();
        //DirectoryInfo[] directories;
        //string exeName = "";

        public MainWindow()
        {
            InitializeComponent();

            SLNPath(@"C:\Users\Kiro\Documents\Visual Studio 2017\Projects");
            SLNName();

        }
        //https://stackoverflow.com/questions/30991331/how-to-navigate-one-folder-up-from-current-file-path
        DirectoryInfo di = new DirectoryInfo(@"C:\Users\Kiro\Documents\Visual Studio 2017\Projects");
        string[] directories = Directory.GetDirectories(@"C:\Users\Kiro\Documents\Visual Studio 2017\Projects");
        string[] files = Directory.GetFiles(@"C:\Users\Kiro\Documents\Visual Studio 2017\Projects", "*.sln");

        public void SLNPath(string sDir)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, "*.sln"))
                {
                    slnPath.Add(f);
                    LIST.ItemsSource = slnPath;
                }
                SLNPath(d);
            }
        }

        public void SLNName()
        {
            foreach (var fi in di.GetFiles("*.exe", SearchOption.AllDirectories))
            {
                slnName.Add(fi.Name);
                slnPath.Add(fi.Name);
                LIST.ItemsSource = slnPath;
            }
        }












        /*public void FindExe(DirectoryInfo[] directories, int j)
        {
            for (int i = 0; i < directories.Length; i++)
            {
                var fileinfo = new DirectoryInfo(@directories[i].FullName);
                FileInfo[] files = fileinfo.GetFiles("*.exe", SearchOption.AllDirectories);

                if (files.Length > 0)
                {

                    //AppPath appPath = new AppPath(exeName);

                    foreach (var item in files)
                    {
                        if (exeName == System.IO.Path.GetFileNameWithoutExtension(item.Name))
                        {
                            pathsList[j].AddPath(item.FullName);
                        }
                    }

                    //pathsList[j].AddAppList(appPath);

                    exeNames.Add(appPath.Name);
                }
            }
        }*/
    }
}
