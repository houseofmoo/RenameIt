
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    public class MainWindowViewModel : Base.ViewModel
    {
        #region private constants
        // content
        private const string RENAME_IT_ACRONYM = "R N";
        #endregion

        #region private fields
        // commands
        private ICommand _menuTitleButtonCommand;

        private Identifiers.Pages _currentPage;
        #endregion

        #region public properties
        public string MenuTitleButtonContent { get { return RENAME_IT_ACRONYM; } }

        public ICommand MenuTitleButtonCommand { get { return _menuTitleButtonCommand; } }

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
            this._menuTitleButtonCommand = new Commands.RelayCommand(this.onTitleButtonClick, true);
    }
        #endregion

        #region methods
        /// <summary>
        /// Occurs on change page command
        /// </summary>
        private void onTitleButtonClick()
        {
            // do something neat
        }
        #endregion
    }
}
