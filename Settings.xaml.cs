using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Nekit
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        static readonly string pathToConfig = $"{Environment.CurrentDirectory}\\Config.JSON";
        ConfigIO configIO = new ConfigIO(path: pathToConfig);
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        private int Profile { get; set; }
        public Settings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ConfigList == null)
                return;

            Frequency.Text = (MainWindow.ConfigList[0].Frequency / 1000).ToString();
            DayDirectory.Text = MainWindow.ConfigList[0].DayFolderPath;
            NightDirectory.Text = MainWindow.ConfigList[0].NightFolderPath;
            DayTime.Text = MainWindow.ConfigList[0].DayTime;
            NightTime.Text = MainWindow.ConfigList[0].NightTime;
            NormalDirectory.Text = MainWindow.ConfigList[0].NormalFolderPath;

            if (MainWindow.ConfigList[0].ExtensionsList != null)
                Extensions.Text = string.Join(" ", MainWindow.ConfigList[0].ExtensionsList);
            
            foreach (char num in MainWindow.ConfigList.Count.ToString())
                ProfileList.Items.Add(num);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                MainWindow.ConfigList[0].Frequency = Convert.ToInt32(Frequency.Text) * 1000;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Раньше здесь был мат");
            }
        }

        private void DayDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            MainWindow.ConfigList[0].DayFolderPath = folderBrowserDialog.SelectedPath;
            DayDirectory.Text = folderBrowserDialog.SelectedPath;
        }

        private void NightDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            MainWindow.ConfigList[0].NightFolderPath = folderBrowserDialog.SelectedPath;
            NightDirectory.Text = folderBrowserDialog.SelectedPath;
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                configIO.SaveData(MainWindow.ConfigList);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Какой-то из полей содержит неправильное значение.\n\t\tТы обосрался");
            }
                
        }
        private void ChooseNormalDirectory_Click(object sender, RoutedEventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            MainWindow.ConfigList[0].NormalFolderPath = folderBrowserDialog.SelectedPath;
            NormalDirectory.Text = folderBrowserDialog.SelectedPath;
        }
        private void DayTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.ConfigList[0].DayTime = DayTime.Text;
        }

        private void NightTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.ConfigList[0].NightTime = NightTime.Text;
        }

        private void Extensions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensions.Text.ToLower() is "ирина" or "ира")
                Process.Start(new ProcessStartInfo
                {
                    FileName = "SHUTDOWN.exe",
                    Arguments = "-s -t 60 -c \"Братанчик, успокойся, ничего с ней не получится =)\nНо shutdown после минуты по расписанию\nFucking slave\"",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });

            else if (Extensions.Text.ToLower() is "никита" or "некит")
                System.Windows.MessageBox.Show("Самый лучший человек на свете\n" +
                    "A.K.A. Спасибо за идею и программу ( ͡° ͜ʖ ͡°)");
            
            MainWindow.ConfigList[0].ExtensionsList = Extensions.Text.Split(" ");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Я ЛЮБЛЮ ЭВЕЛИНУ =)\n" +
                "Я ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ПУУУУТУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\nЯ ЛЮБЛЮ ЭВЕЛИНУ =)\n");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs eblan)
        {
            
        }

        private void ProfileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Profile = ProfileList.SelectedIndex;
        }
    }
}
