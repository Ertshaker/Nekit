using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Nekit
{
    /// <summary>
    /// Логика взаимодействия для FileSort.xaml
    /// </summary>
    public partial class FileSort : Window
    {
        private string path { get; set; }
        private string[] files;
        private Dictionary<string, string> extensions = new Dictionary<string, string>();
        public FileSort()
        {
            InitializeComponent();
        }
 
        private void GetFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog() { AutoUpgradeEnabled=true};
            dialog.ShowDialog();
            (DirectoryTextBox.Text, path) = (dialog.SelectedPath, dialog.SelectedPath);
            files = Directory.GetFiles(path);
            
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                if (!extensions.ContainsKey(extension))
                    extensions.Add(extension, $"{extension} files");
            }
        }
        private void Sort_Files(object sender, RoutedEventArgs e)
        {
            foreach (var extension in extensions)
                Directory.CreateDirectory($"{path}\\{extension.Value}");

            foreach (string file in Directory.GetFiles(path))
            {
                string destination = path + "\\" + extensions[Path.GetExtension(file)];
                File.Move(file, destination + "\\" + Path.GetFileName(file), true);
            }
        }
    }
}
