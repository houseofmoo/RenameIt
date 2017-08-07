
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    public class MainWindowViewModel : Base.ViewModel
    {
        #region private constants
        private const string SETTINGS = "Settings";
        private const string RENAME_IT = "RenameIt";
        #endregion

        #region private fields
        private ICommand _changePage;
        private Identifiers.Pages _currentPage;
        private string _changePageButtonContent = SETTINGS;
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

        /// <summary>
        /// The content of the change page button
        /// </summary>
        public string ChangePageButtonContent
        {
            get { return _changePageButtonContent; }
            set
            {
                if (_changePageButtonContent == value)
                    return;
                _changePageButtonContent = value;
                OnPropertyChanged(nameof(ChangePageButtonContent));
            }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            // set beginning page
            this._currentPage = Identifiers.Pages.RenameIt;

            // set change page command
            this._changePage = new Commands.RelayCommand(this.onChangePage, true);
        }
        #endregion

        #region methods
        /// <summary>
        /// Occurs on change page command
        /// </summary>
        private void onChangePage()
        {
            // update settings
            if (this.CurrentPage == Identifiers.Pages.Settings)
                updateSettingsFile();

            // change page
            ChangePage();
        }

        /// <summary>
        /// Changes the page between RenameIt and Settings
        /// </summary>
        private void ChangePage()
        {
            if (this.CurrentPage == Identifiers.Pages.RenameIt)
            {
                // switch to settings page
                this.CurrentPage = Identifiers.Pages.Settings;
                this.ChangePageButtonContent = RENAME_IT;
            }
            else
            {
                // switch to rename it main page
                this.CurrentPage = Identifiers.Pages.RenameIt;
                this.ChangePageButtonContent = SETTINGS;
            }
        }

        /// <summary>
        /// Updates the settings XML file using the global settings state
        /// </summary>
        private void updateSettingsFile()
        {
            // update changes to settings
            // update settings file - to do, need better way
            Properties.Settings.Default.GetEpisodeTitles = User.Settings.Get().GetEpisodeTitles;
            Properties.Settings.Default.IncludeSubtitles = User.Settings.Get().IncludeSubtitles;
            Properties.Settings.Default.DeleteNonMediaFiles = User.Settings.Get().DeleteNonMediaFiles;
            Properties.Settings.Default.Save();
            //Properties.Settings.Default.SearchSubDirectories = this.SearchSubDirectories;
        } 
        #endregion
    }
}
