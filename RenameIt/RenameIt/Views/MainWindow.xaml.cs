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
            this.DataContext = new ViewModels.MainWindow.ContentViewModel(this.listView,
                this.ShowNameTextBox, this.SeasonTextBox, this.EpisodeStartNumberTextBox);
        }
    }
}
