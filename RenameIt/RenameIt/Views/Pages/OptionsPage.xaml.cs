using System.Windows.Controls;

namespace RenameIt.Views.Pages
{
    /// <summary>
    /// Interaction logic for OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OptionsViewModel();
        }
    }
}
