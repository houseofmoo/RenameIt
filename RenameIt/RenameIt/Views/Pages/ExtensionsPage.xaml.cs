using System;
using System.Collections.Generic;
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

namespace RenameIt.Views.Pages
{
    /// <summary>
    /// Interaction logic for ExtensionsPage.xaml
    /// </summary>
    public partial class ExtensionsPage : Page
    {
        public ExtensionsPage()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.ExtensionsViewModel();
        }
    }
}
