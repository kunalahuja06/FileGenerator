using FileGenerator.Core.Contracts;
using FileGenerator.Core.Services;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileGenerator
{
    /// <summary>
    /// Interaction logic for PathControl.xaml
    /// </summary>
    public partial class PathControl : UserControl
    {
        private string _pattern;

        private readonly IFolderService _folderService;
        private readonly HttpClient _httpClient;
        public PathControl(string pattern)
        {
            _httpClient = new HttpClient();
            _folderService = new FolderService(_httpClient);
            InitializeComponent();
            _pattern = pattern;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string path = txtPath.Text;

            if (!string.IsNullOrEmpty(path))
            {
                path = Path.Combine(path, _pattern);
                bool generateFolder = await CreateFolder(path, _pattern);
                if (generateFolder)
                {
                    MessageBox.Show("Files generated successfully");
                    CloseWindow();
                }
            }
            else
            {
                MessageBox.Show("Please enter a path");
            }
        }

        private async Task<bool> CreateFolder(string path, string pattern)
        {
            bool generated = false;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                generated = await _folderService.GetPatternFiles(pattern, path);
                return generated;
            }
            else
            {
                MessageBox.Show("Folder already exists");
                return generated;
            }  
        }

        private void CloseWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.A && ModifierKeys.Control== Keyboard.Modifiers)
            {
               if(e.Key == Key.Back || e.Key == Key.Delete)
               {
                    txtPath.Clear();
                    e.Handled = true;
                    return;
               }
               txtPath.SelectAll();
               e.Handled = true;
            }
            else if (e.Key == Key.C && ModifierKeys.Control == Keyboard.Modifiers)
            {
                txtPath.Copy();
                e.Handled = true;
            }
            else if(e.Key == Key.V && ModifierKeys.Control == Keyboard.Modifiers)
            {
                txtPath.Paste();
                e.Handled = true;
            }
            else if(e.Key == Key.X && ModifierKeys.Control == Keyboard.Modifiers)
            {
                txtPath.Cut();
                e.Handled = true;
            }
            else if(e.Key == Key.Z && ModifierKeys.Control == Keyboard.Modifiers)
            {
                txtPath.Undo();
                e.Handled = true;
            }
            else if(e.Key == Key.Y && ModifierKeys.Control == Keyboard.Modifiers)
            {
                txtPath.Redo();
                e.Handled = true;
            }
            else if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (txtPath.SelectionLength > 0)
                {
                    txtPath.SelectedText = "";
                }
                else
                {
                    if (txtPath.CaretIndex > 0)
                    {
                        txtPath.Text = txtPath.Text.Remove(txtPath.CaretIndex - 1, 1);
                        txtPath.CaretIndex = txtPath.Text.Length;
                    }
                }
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
