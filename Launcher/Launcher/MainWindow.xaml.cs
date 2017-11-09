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
using System.Diagnostics;

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sDir = @"D:\purnolu15";

        List<string> Path = new List<string>();
        List<string> slnName = new List<string>();
        List<string> exeName = new List<string>();
        List<string> Directoryy = new List<string>();

        DirectoryInfo di = new DirectoryInfo(@"D:\purnolu15");
        string[] directories = Directory.GetDirectories(@"D:\purnolu15");
        string[] directoriess = Directory.GetDirectories(@"D:\purnolu15", "*");

        //string folder = new DirectoryInfo(@"D:\purnolu15").Name;


        public MainWindow()
        {
            InitializeComponent();

            foreach (string dd in directoriess)
            {
                Directoryy.Add(dd);
                PATH.ItemsSource = Directoryy;
            }

            //SLNPath(sDir);
            //EXEPath(sDir);
            //SLNName();
            //EXEName();

        }



        public void SLNPath(string sDir)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, "*.sln"))
                {
                    Path.Add(f);
                    PATH.ItemsSource = Path;
                }
                SLNPath(d);
            }
        }
        public void EXEPath(string sDir)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, "*.exe"))
                {
                    Path.Add(f);
                    PATH.ItemsSource = Path;
                }
                EXEPath(d);
            }
        }
        public void FindEXEPath(string sDir, string fpath)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, fpath))
                {
                    Process.Start(f);
                }
                FindEXEPath(d, fpath);
            }
        }
        public void FindSLNPath(string sDir, string fpath)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, fpath))
                {
                    Process.Start(f);
                }
                FindSLNPath(d, fpath);
            }
        }
        public void SLNName()
        {
            foreach (var fi in di.GetFiles("*.sln", SearchOption.AllDirectories))
            {
                slnName.Add(fi.Name);
                SLN.ItemsSource = slnName;
            }
        }
        public void EXEName()
        {
            foreach (var fi in di.GetFiles("*.exe", SearchOption.AllDirectories))
            {
                exeName.Add(fi.Name);
                EXE.ItemsSource = exeName;
            }
        }

        private void RunExe_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = EXE.SelectedIndex; //name
            var fpath = exeName[EXE.SelectedIndex];

            FindEXEPath(sDir, fpath);
        }
        private void RunSln_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = SLN.SelectedIndex; //name
            var fpath = slnName[SLN.SelectedIndex];

            FindSLNPath(sDir, fpath);
        }
        private void AddCVS_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItems = SLN.SelectedIndex; //name
            //var fpath = slnName[SLN.SelectedIndex];
        }
    }
}
