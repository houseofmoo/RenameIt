using System.Windows;

namespace RenameIt.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // connect to the main windows view model
            this.DataContext = new ViewModels.MainWindowViewModel();
        }

        /// <summary>
        /// On main window closing, save the user settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {   
            Properties.Settings.Default.Save();
        }
    }
}
