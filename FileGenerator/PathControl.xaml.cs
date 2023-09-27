using System;
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

        private string _path;

        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }

        public PathControl(string pattern)
        {
            InitializeComponent();
            _pattern = pattern;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            string userInput = txtPath.Text;

            if(!string.IsNullOrEmpty(userInput))
            {
                bool generated =  CreateFolder(userInput);
                if (generated)
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

        private bool CreateFolder(string path)
        {

            if (_pattern == "Repository Pattern")
            {
                bool created = CreateRepositoryFolder(path);
                return created;
            }
            else if (_pattern == "Unit of Work Pattern")
            {
                bool created = CreateUnitOfWorkFolder(path);
                return created;
            }
            else
            {
                MessageBox.Show("Please select a pattern");
                return false;
            }

        }

        private bool CreateUnitOfWorkFolder(string path)
        {
            string folderName = "UnitOfWork";
            string folderPath = System.IO.Path.Combine(path, folderName);
            try
            {
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                    bool generated = GenerateUnitOfWorkFiles(folderPath);
                    return generated;
                }
                else
                {
                    MessageBox.Show("Folder already exists");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CreateRepositoryFolder(string path)
        {
            string folderName = "Repository";
            string folderPath = System.IO.Path.Combine(path, folderName);
            try
            {
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                    bool generated = GenerateRepositoryFiles(folderPath);
                    return generated;
                }
                else
                {
                    MessageBox.Show("Folder already exists");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool GenerateRepositoryFiles(string path)
        {
            try
            {
                string[] files = new string[] { "IRepository.cs", "Repository.cs" };
                foreach (string file in files)
                {
                    string filePath = System.IO.Path.Combine(path, file);
                    if (!System.IO.File.Exists(filePath))
                    {
                        System.IO.File.WriteAllText(filePath, GetRepositoryFileContent(file));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private string GetRepositoryFileContent(string fileName)
        {
            string content = string.Empty;
            switch (fileName)
            {
                case "IRepository.cs":
                    content = @"using System;"
                    + Environment.NewLine + Environment.NewLine + @"namespace YOUR_NAMESPACE"
                    + Environment.NewLine + @"{"
                    + Environment.NewLine + @"    public interface IRepository<T> where T : class"
                    + Environment.NewLine + @"    {"
                    + Environment.NewLine + @"    }"
                    + Environment.NewLine + @"}";
                    break;

                case "Repository.cs":
                    content = @"using System;"
                    + Environment.NewLine + Environment.NewLine + @"namespace YOUR_NAMESPACE"
                    + Environment.NewLine + @"{"
                    + Environment.NewLine + @"    public class Repository<T> : IRepository<T> where T : class"
                    + Environment.NewLine + @"    {"
                    + Environment.NewLine + @"         public Repository()"
                    + Environment.NewLine + @"         {"
                    + Environment.NewLine + @"             // constructor code goes here"
                    + Environment.NewLine + @"         }"
                    + Environment.NewLine + @"    }"
                    + Environment.NewLine + @"}";
                    break;
            }
            return content;
        }

        private bool GenerateUnitOfWorkFiles(string path)
        {
            try
            {
                string[] files = new string[] { "IUnitOfWork.cs", "UnitOfWork.cs" };
                foreach (string file in files)
                {
                    string filePath = System.IO.Path.Combine(path, file);
                    if (!System.IO.File.Exists(filePath))
                    {
                        System.IO.File.WriteAllText(filePath, GetUnitOfWorkFileContent(file));
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private string GetUnitOfWorkFileContent(string file)
        {
            string content = string.Empty;
            switch (file)
            {
                case "IUnitOfWork.cs":
                    content = @"using System;"
                    + Environment.NewLine + Environment.NewLine + @"namespace YOUR_NAMESPACE"
                    + Environment.NewLine + @"{"
                    + Environment.NewLine + @"    public interface IUnitOfWork : IDisposable"
                    + Environment.NewLine + @"    {"
                    + Environment.NewLine + @"    }"
                    + Environment.NewLine + @"}";
                    break;

                case "UnitOfWork.cs":
                    content = @"using System;"
                    + Environment.NewLine + Environment.NewLine + @"namespace YOUR_NAMESPACE"
                    + Environment.NewLine + @"{"
                    + Environment.NewLine + @"    public class UnitOfWork : IUnitOfWork"
                    + Environment.NewLine + @"    {"
                    + Environment.NewLine + @"         public UnitOfWork()"
                    + Environment.NewLine + @"         {"
                    + Environment.NewLine + @"             // constructor code goes here"
                    + Environment.NewLine + @"         }"
                    + Environment.NewLine + Environment.NewLine + @"         public void Dispose()"
                    + Environment.NewLine + @"         {"
                    + Environment.NewLine + @"             // dispose code goes here"
                    + Environment.NewLine + @"         }"
                    + Environment.NewLine + @"    }"
                    + Environment.NewLine + @"}";
                    break;
            }
            return content;
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
