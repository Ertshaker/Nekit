using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime;
using System.Windows;
using System.IO;

namespace Nekit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string pathToConfig = $"{Environment.CurrentDirectory}\\Config.json";
        public static int Profile;
        public static BindingList<Config> ConfigList;
        ChangeImage changeImage;
        enum State
        {
            Day,
            Night,
            Normal
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NormalMode_Checked(object sender, RoutedEventArgs e)
        {
            ConfigList[0].State = Config.States.Normal;
        }

        private void DayNightMode_Checked(object sender, RoutedEventArgs e)
        {
            ConfigList[0].State = Config.States.DayNight;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //MessageBox.Show("Нееет, не закрывайте программу =(\n" +"Пожалуйста, напишите автору что пошло не так.." + "\n\t(Или вас сошлют в СФУ)");
        }
        private void TaskbarIcon_TrayLeftMouseDown(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigIO configIO = new ConfigIO(pathToConfig);
            ConfigList = configIO.LoadData();
            if (ConfigList[0].State == Config.States.Normal)
                NormalRadioButton.IsChecked = true;
            else
                DayNightRadioButton.IsChecked = true;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (ConfigList[0].State is (Config.States) 2)
            {
                if (ConfigList[0].Frequency == 0 || ConfigList[0].ExtensionsList[0] == "" || ConfigList[0].NormalFolderPath == null)
                    MessageBox.Show("Какие-то из этих настроек не заполнены: Частота, Обычная папка, Список расширений. Исправься");
                else
                {
                    if (Directory.GetFiles(ConfigList[0].NormalFolderPath).Length == 0)
                    {
                        MessageBox.Show("Папка, где должны быть картинки\nоказалось пустой, либо файлы не прошли отбор. Иди-ка ты нахер");
                        return;
                    }
                    (StartButton.IsEnabled, StopButton.IsEnabled) = (false, true);
                    changeImage = new ChangeImage(
                        ConfigList[0].Frequency, 
                        ConfigList[0].NormalFolderPath
                        );
                    changeImage.Start();
                }
            }

            else if (ConfigList[0].State is (Config.States) 1)
            {
                if (ConfigList[0].Frequency == 0 || ConfigList[0].ExtensionsList == null
                    || ConfigList[0].DayFolderPath == null || ConfigList[0].NightFolderPath == null || ConfigList[0].DayTime == null || ConfigList[0].NightTime == null)
                    MessageBox.Show("Какие-то из этих настроек не заполнены: Частота, Дневная и ночная папка, Дневное и ночное время, Список расширений). Исправься");
                else
                {
                    if (Directory.GetFiles(ConfigList[0].DayFolderPath).Length == 0 || Directory.GetFiles(ConfigList[0].NightFolderPath).Length == 0)
                    {
                        MessageBox.Show("Папка, где должны быть картинки\nоказалось пустой, либо файлы не прошли отбор. Иди-ка ты нахер");
                        return;
                    }
                    changeImage = new ChangeImage(
                        ConfigList[0].Frequency, 
                        ConfigList[0].DayFolderPath, 
                        ConfigList[0].NightFolderPath, 
                        ConfigList[0].DayTime, 
                        ConfigList[0].NightTime
                        );
                    changeImage.Start();
                    (StartButton.IsEnabled, StopButton.IsEnabled) = (false, true);
                }
            }
            
            
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            changeImage.Stop();
            (StartButton.IsEnabled, StopButton.IsEnabled) = (true, false);
        }

        private void Settings_Click(object sender, RoutedEventArgs e) => new Settings().ShowDialog();
        private void Window_Closing(object sender, CancelEventArgs e) 
        {
            new ConfigIO(pathToConfig).SaveData(ConfigList);
            changeImage?.Stop();
        } 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (JokeImage.Opacity == 0 && ConfigList[0].ExtensionsList[0] is "Пылесос" or "пылесос")
            {
                JokeImage.Opacity = 100;
                StartButton.Opacity = 0;
                StopButton.Opacity = 0;
                DayNightRadioButton.Opacity = 0;
                NormalRadioButton.Opacity = 0;
                label.Opacity = 0;
                MessageBox.Show("Упсс...");
            }
            else
            {
                JokeImage.Opacity = 0;
                StartButton.Opacity = 100;
                StopButton.Opacity = 100;
                DayNightRadioButton.Opacity = 100;
                NormalRadioButton.Opacity = 100;
                label.Opacity = 100;
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new FileSort().ShowDialog();
        }
    }
}
