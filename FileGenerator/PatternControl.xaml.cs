using Microsoft.VisualStudio.PlatformUI;
using System.Windows;
using System.Windows.Controls;

namespace FileGenerator
{
    /// <summary>
    /// Interaction logic for PatternControl.xaml
    /// </summary>
    public partial class PatternControl : UserControl
    {
        private Window _parentWindow;
        public PatternControl()
        {
            InitializeComponent();
        }

        public void SetParentWindow(Window window)
        {
            _parentWindow = window;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
            if (item != null)
            {
                _parentWindow.Close();
                PathControl PathControl = new PathControl(item.Content.ToString());
                Window window = new Window();
                window.Width = 650;
                window.Height = 150;
                window.Title = $"Path to generate {item.Content} files";
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Content = PathControl;
                window.Focus();
                window.Activate();
                window.Show();
            }
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var actualHeight = _parentWindow.ActualHeight;

            ListBox listBox = (ListBox)sender;
            var listBoxHeight = listBox.ActualHeight;

            ListBoxItem[] items = new ListBoxItem[listBox.Items.Count];
            var newHeight = 0;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                items[i] = (ListBoxItem)listBox.Items[i];
                newHeight += (int)items[i].ActualHeight;
            }

            _parentWindow.Height = newHeight + 50;
        }

    }
}
