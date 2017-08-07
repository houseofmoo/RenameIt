using System.Windows.Controls;

namespace RenameIt.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.SettingsViewModel();
        }
    }
}
