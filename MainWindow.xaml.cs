using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Automation;
using System.Windows.

namespace Nekit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string userRoot = "HKEY_CURRENT_USER\\Control Panel\\Desktop";
        public MainWindow()
        {

            InitializeComponent();
            var commandBinding = new CommandBinding();        // создаем привязку команды
            commandBinding.Command = ApplicationCommands.New; // устанавливаем команду
            commandBinding.Executed += SetImage;              // устанавливаем метод, который будет выполняться при вызове команды
            Loser.CommandBindings.Add(commandBinding);        // добавляем привязку к коллекции привязок элемента Button
        }
        private void SetImage(object sender, ExecutedRoutedEventArgs estring)
        {
            Registry.SetValue(userRoot, "WallPaper", "C:\\Users\\maxim\\Desktop\\IMG_20201102_135028.jpg");
            Process.Start(new ProcessStartInfo
            {
                FileName = "PowerShell.exe",
                Arguments = "RUNDLL32.EXE USER32.DLL,UpdatePerUserSystemParameters ,1 ,True",
                WindowStyle = ProcessWindowStyle.Hidden,
            });
        }
    }
}


/*public class Program
{
    static void SetImage(string userRoot)
    {
        Registry.SetValue(userRoot, "WallPaper", "C:\\Users\\maxim\\Desktop\\IMG_20201102_135028.jpg");
        Process.Start(new ProcessStartInfo
        {
            FileName = "PowerShell.exe",
            UseShellExecute = false,
            Arguments = "RUNDLL32.EXE USER32.DLL,UpdatePerUserSystemParameters ,1 ,True",
            WindowStyle = ProcessWindowStyle.Hidden,
        });
    }

    static void Main(string[] args)
    {
        // "C:\\Users\\maxim\\Desktop\\IMG_20201102_135028.jpg"
        // "K:\\дичь\\amongus.png"
        // RUNDLL32.EXE USER32.DLL,UpdatePerUserSystemParameters ,1 ,True
        const string userRoot = "HKEY_CURRENT_USER\\Control Panel\\Desktop";
        SetImage(userRoot);
    }
}*/
