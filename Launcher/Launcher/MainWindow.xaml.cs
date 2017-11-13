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
using System.IO;
using System.Diagnostics;

namespace Launcher_V2
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sDir = @"C:\Users\Kiro\Source\Repos";
        string TxtName;

        DirectoryInfo di = new DirectoryInfo(@"C:\Users\Kiro\Source\Repos");

        string[] directories = Directory.GetDirectories(@"C:\Users\Kiro\Source\Repos", "*.sln");

        List<string> FolderNames = new List<string>();
        List<string> FolderPaths = new List<string>();
        List<string> SlnPaths = new List<string>();
        List<string> ExePaths = new List<string>();
        List<string> ExeNames = new List<string>();

        List<string> FinalExePath = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            Skryt();
            DirectoryShow(sDir);
        }

        public void Skryt()
        {
            FileName.Visibility = Visibility.Hidden;
            Textbox.Visibility = Visibility.Hidden;
            RunExe.Visibility = Visibility.Hidden;
            RunSln.Visibility = Visibility.Hidden;
            CreateTxt.Visibility = Visibility.Hidden;
            SaveTxt.Visibility = Visibility.Hidden;
            DeleteTxt.Visibility = Visibility.Hidden;
            DeleteDir.Visibility = Visibility.Hidden;
        }
        public void Zobrazit()
        {
            FileName.Visibility = Visibility.Visible;
            Textbox.Visibility = Visibility.Visible;
            RunExe.Visibility = Visibility.Visible;
            RunSln.Visibility = Visibility.Visible;
            CreateTxt.Visibility = Visibility.Visible;
            SaveTxt.Visibility = Visibility.Visible;
            DeleteTxt.Visibility = Visibility.Visible;
            DeleteDir.Visibility = Visibility.Visible;
        }

        public void DirectoryShow(string sDir) //ZOBRAZENÍ JMEN SLOŽEK VE SLOŽCE /REPOS
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, "*.sln"))
                {
                    SlnPaths.Add(f); // SLN CESTY
                    FolderPaths.Add(System.IO.Path.GetDirectoryName(f)); // SLOZKY CESTY

                    string FolderName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(f)); // ZÍSKÁNÍ SAMOSTATNÉHO JMÉNA
                    FolderNames.Add(FolderName); // PŘIDÁNÍ DO LISTU
                    DIRS.ItemsSource = FolderNames; // VÝPIS DO LISTVIEW
                }
                DirectoryShow(d);
            }


        }

        public void RunSelectedEXE(string sDir, string ExeName)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, ExeName + ".exe"))
                {
                    Process.Start(f);
                }
                RunSelectedEXE(d, ExeName);
            }
        }
        public void AddTxt()
        {
            TxtName = FolderNames[DIRS.SelectedIndex];
            var fpath = FolderPaths[DIRS.SelectedIndex];

            using (FileStream fs = File.Create(fpath + "/" + TxtName + "_readme.txt"))
            {
                /*Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                fs.Write(info, 0, info.Length);*/
            }
        }
        public void ReadTxt()
        {
            TxtName = FolderNames[DIRS.SelectedIndex];
            var fpath = FolderPaths[DIRS.SelectedIndex];

            if (File.Exists(fpath + "/" + TxtName + "_readme.txt"))
            {
                Textbox.Text = File.ReadAllText(fpath + "/" + TxtName + "_readme.txt");
            }
        }
        public void SaveeTxt()
        {
            TxtName = FolderNames[DIRS.SelectedIndex];
            var fpath = FolderPaths[DIRS.SelectedIndex];
            string BoxText = Textbox.Text;

            if (File.Exists(fpath + "/" + TxtName + "_readme.txt"))
            {
                File.WriteAllText(fpath + "/" + TxtName + "_readme.txt", BoxText);
            }
        }
        public void DeleteeTxt()
        {
            TxtName = FolderNames[DIRS.SelectedIndex];
            var fpath = FolderPaths[DIRS.SelectedIndex];

            if (File.Exists(fpath + "/" + TxtName + "_readme.txt"))
            {
                File.Delete(fpath + "/" + TxtName + "_readme.txt");
            }

            //DIRS.ItemsSource = fpath + "/" + TxtName + "_readme.txt";
        }
        public void DeleteeDir()
        {
            TxtName = FolderNames[DIRS.SelectedIndex];
            var fpath = FolderPaths[DIRS.SelectedIndex];

            if (Directory.Exists(fpath))
            {
                Directory.Delete(fpath, true);
            }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Zobrazit();
            FileName.Content = FolderNames[DIRS.SelectedIndex]; // VÝPIS PODLE VYBRANÉ SLOŽKY
            ReadTxt();
        }
        private void RunSln_Click(object sender, RoutedEventArgs e)
        {
            var fpath = SlnPaths[DIRS.SelectedIndex]; // CESTA VYBRANÁ PODLE INDEXU
            Process.Start(fpath); // SPUŠTĚNÍ .SLN
        }
        private void RunExe_Click(object sender, RoutedEventArgs e)
        {
            var fpath = FolderNames[DIRS.SelectedIndex]; // NÁZEV SLOŽKY --> NÁZEV EXE SOUBORU
            RunSelectedEXE(sDir, fpath);
        }
        private void CreateTxt_Click(object sender, RoutedEventArgs e)
        {
            var fpath = FolderNames[DIRS.SelectedIndex];

            AddTxt();
        }
        private void SaveTxt_Click(object sender, RoutedEventArgs e)
        {
            SaveeTxt();
        }
        private void DeleteDir_Click(object sender, RoutedEventArgs e)
        {
            DeleteeDir();
        }
        private void DeleteTxt_Click(object sender, RoutedEventArgs e)
        {
            DeleteeTxt();
        }
    }
}
