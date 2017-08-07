
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    public class MainWindowViewModel : Base.ViewModel
    {
        #region private fields
        private ICommand _changePage;
        private Identifiers.Pages _currentPage;
        #endregion

        #region public properties
        /// <summary>
        /// Holds the current page we're viewing
        /// </summary>
        public Identifiers.Pages CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value)
                    return;
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        /// <summary>
        /// Command to change between pages
        /// </summary>
        public ICommand ChangePageCommand { get { return this._changePage; } }
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            // video settings
            foreach (var ext in Properties.Settings.Default.VideoExtensions)
                User.Settings.VideoExtensions.Add(ext);

            // subtitle settings
            foreach (var ext in Properties.Settings.Default.SubtitleExtensions)
                User.Settings.SubtitleExtensions.Add(ext);

            // set beginning page
            this._currentPage = Identifiers.Pages.RenameIt;

            // set change page command
            this._changePage = new Commands.RelayCommand(this.ChangePage, true);
        } 
        #endregion

        /// <summary>
        /// test method
        /// </summary>
        public void ChangePage()
        {
            this.CurrentPage = this.CurrentPage == Identifiers.Pages.RenameIt ? Identifiers.Pages.Settings : Identifiers.Pages.RenameIt;
        }
    }
}
