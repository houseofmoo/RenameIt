
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    public class MainWindowViewModel : Base.ViewModel
    {
        #region private constants
        // content
        private const string RENAME_IT_ACRONYM = "R N";
        private const string RENAME_IT = "RenameIt";
        private const string EXTENSTIONS = "Extensions";
        private const string OPTIONS = "Options";

        // button formats
        private const int MENU_BUTTON_VISIBLE = 100;
        private const int MENU_BUTTON_INVISIBLE = 0;

        // menu formats
        private const int MENU_SHOWN = 100;
        private const int MENU_HIDDEN = 35;
        #endregion

        #region private fields
        private int _menuWidth = MENU_SHOWN;
        private int _menuButtonOpacity = MENU_BUTTON_VISIBLE;

        // commands
        private ICommand _menuTitleButtonCommand;
        private ICommand _renameItButtonCommand;
        private ICommand _extensionsButtonCommand;
        private ICommand _optionsButtonCommand;

        private Identifiers.Pages _currentPage;
        #endregion

        #region public properties
        // content
        public string MenuTitleButtonContent { get { return RENAME_IT_ACRONYM; } }
        public string RenameItButtonContent { get { return RENAME_IT; } }
        public string ExtensionsButtonContent { get { return EXTENSTIONS; } }
        public string OptionsButtonContent { get { return OPTIONS; } }

        // dimensions
        public int MenuWidth
        {
            get { return _menuWidth; }
            set
            {
                if (_menuWidth == value)
                    return;
                _menuWidth = value;
                OnPropertyChanged(nameof(MenuWidth));
            }
        }

        // opacity
        public int MenuButtonOpacity
        {
            get { return _menuButtonOpacity; }
            set
            {
                if (_menuButtonOpacity == value)
                    return;
                _menuButtonOpacity = value;
                OnPropertyChanged(nameof(MenuButtonOpacity));
            }
        }

        // commands
        public ICommand MenuTitleButtonCommand { get { return _menuTitleButtonCommand; } }
        public ICommand RenameItButtonCommand { get { return _renameItButtonCommand; } }
        public ICommand ExtensionsButtonCommand { get { return _extensionsButtonCommand; } }
        public ICommand OptionsButtonCommand { get { return _optionsButtonCommand; } }

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
            this._renameItButtonCommand = new Commands.RelayCommand(this.onRenameItButtonClick, true);
            this._extensionsButtonCommand = new Commands.RelayCommand(this.onExtensionsButtonClick, true);
            this._optionsButtonCommand = new Commands.RelayCommand(this.onOptionsButtonClick, true);
    }
        #endregion

        #region methods
        /// <summary>
        /// Occurs on change page command
        /// </summary>
        private void onTitleButtonClick()
        {
            // hide the menu
            if (this.MenuWidth == MENU_SHOWN)
            {
                this.MenuWidth = MENU_HIDDEN;
                this.MenuButtonOpacity = MENU_BUTTON_INVISIBLE;
            }
            else
            {
                this.MenuWidth = MENU_SHOWN;
                this.MenuButtonOpacity = MENU_BUTTON_VISIBLE;
            }
        }

        private void onRenameItButtonClick()
        {
            this.CurrentPage = Identifiers.Pages.RenameIt;
        }

        private void onExtensionsButtonClick()
        {
            this.CurrentPage = Identifiers.Pages.Extensions;
        }

        private void onOptionsButtonClick()
        {
            this.CurrentPage = Identifiers.Pages.Options;
        }
        #endregion
    }
}
