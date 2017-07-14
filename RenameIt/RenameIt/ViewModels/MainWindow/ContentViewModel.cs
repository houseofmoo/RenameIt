using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RenameIt.ViewModels.MainWindow
{
    class ContentViewModel : Base.ViewModel
    {
        #region private constants
        // text box default values
        private const string ShowName = "Show Name";
        private const string Season = "Season";
        private const string EpisodeStartNumber = "Episode Start Number";

        // button text default values
        private const string Directory = "Select Directory";
        private const string Preview = "Preview";
        private const string Confirm = "Confirm";
        #endregion

        #region private fields
        // buttons
        private ButtonViewModel _directoryButton;
        private ButtonViewModel _previewButton;
        private ButtonViewModel _confirmButton;

        // text boxes
        private TextBox _showNameTextBox;
        private string _showNameTextBoxText;
        private TextBox _seasonTextBox;
        private string _seasonTextBoxText;
        private TextBox _episodeStartNumberTextBox;
        private string _episodeStartNumberTextBoxText;

        // list view
        private ListView _listView;
        private double _listViewColumnWidth;

        // data
        private ObservableCollection<Directory.ItemViewModel> _items;
        #endregion

        #region public properties
        /// <summary>
        /// Directory button.
        /// </summary>
        public ButtonViewModel DirectoryButton
        {
            get { return _directoryButton; }
            set
            {
                if (_directoryButton == value) return;
                _directoryButton = value;
                OnPropertyChanged(nameof(DirectoryButton));
            }
        }

        /// <summary>
        /// Returns directory button content.
        /// </summary>
        public string DirectoryButtonContent { get { return Directory; } }

        /// <summary>
        /// Preview button.
        /// </summary>
        public ButtonViewModel PreviewButton
        {
            get { return _previewButton; }
            set
            {
                if (_previewButton == value) return;
                _previewButton = value;
                OnPropertyChanged(nameof(PreviewButton));
            }
        }

        /// <summary>
        /// Returns preview button content
        /// </summary>
        public string PreviewButtonContent { get { return Preview; } }

        /// <summary>
        /// Confirm button.
        /// </summary>
        public ButtonViewModel ConfirmButton
        {
            get { return _confirmButton; }
            set
            {
                if (_confirmButton == value) return;
                _confirmButton = value;
                OnPropertyChanged(nameof(ConfirmButton));
            }
        }

        /// <summary>
        /// Returns confirm button content.
        /// </summary>
        public string ConfirmButtonContent { get { return Confirm; } }

        /// <summary>
        /// The show name text boxes text. If text is ever empty, sets value to "Show Name".
        /// </summary>
        public string ShowNameTextBoxText
        {
            get { return _showNameTextBoxText; }
            set
            {
                if (value == _showNameTextBoxText) return;
                _showNameTextBoxText = value == string.Empty ? ShowName : value;
                OnPropertyChanged(nameof(ShowNameTextBoxText));
            }
        }

        /// <summary>
        /// The season text boxes text. If text is ever empty, sets value to "Season".
        /// </summary>
        public string SeasonTextBoxText
        {
            get { return _seasonTextBoxText; }
            set
            {
                if (value == _seasonTextBoxText) return;
                _seasonTextBoxText = value == string.Empty ? Season : value;
                OnPropertyChanged(nameof(SeasonTextBoxText));
            }
        }

        /// <summary>
        /// The episdoe start number text boxes text. If text is ever empty, sets value to "Episode Start Number".
        /// </summary>
        public string EpisodeStartNumberTextBoxText
        {
            get { return _episodeStartNumberTextBoxText; }
            set
            {
                if (value == _episodeStartNumberTextBoxText) return;
                _episodeStartNumberTextBoxText = value == string.Empty ? EpisodeStartNumber : value;
                OnPropertyChanged(nameof(EpisodeStartNumberTextBoxText));
            }
        }

        /// <summary>
        /// Returns the current list view column width.
        /// </summary>
        public double ListViewColumnWidth
        {
            get { return _listViewColumnWidth; }
            set
            {
                if (_listViewColumnWidth == value) return;
                _listViewColumnWidth = value;
                OnPropertyChanged(nameof(ListViewColumnWidth));
            }
        }

        /// <summary>
        /// List of items found in the directory
        /// </summary>
        public ObservableCollection<Directory.ItemViewModel> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContentViewModel(ListView listView, TextBox showNameTextBox, TextBox seasonTextBox, TextBox episodeStartNumberTextBox)
        {
            // set UI elements we need to manipulate
            this._listView = listView;
            this._showNameTextBox = showNameTextBox;
            this._seasonTextBox = seasonTextBox;
            this._episodeStartNumberTextBox = episodeStartNumberTextBox;

            // default window settings
            this.ListViewColumnWidth = 250f;
            this.ShowNameTextBoxText = ShowName;
            this.SeasonTextBoxText = Season;
            this.EpisodeStartNumberTextBoxText = EpisodeStartNumber;

            // create items data structure, this holds the bulk of the data
            this.Items = new ObservableCollection<Directory.ItemViewModel>();

            // pass mutator functions to button view model which contain the commands
            this.DirectoryButton = new ButtonViewModel(() => directoryButtonClick(), true);
            this.PreviewButton = new ButtonViewModel(null, false);
            this.ConfirmButton = new ButtonViewModel(null, false);

            // on keyboard or mouse focus, we select all text in the text box
            this._showNameTextBox.GotKeyboardFocus += textBoxOnFocusSelectALlText;
            this._showNameTextBox.GotMouseCapture += textBoxOnFocusSelectALlText;
            this._seasonTextBox.GotKeyboardFocus += textBoxOnFocusSelectALlText;
            this._seasonTextBox.GotMouseCapture += textBoxOnFocusSelectALlText;
            this._episodeStartNumberTextBox.GotKeyboardFocus += textBoxOnFocusSelectALlText;
            this._episodeStartNumberTextBox.GotMouseCapture += textBoxOnFocusSelectALlText;
        }
        #endregion

        #region private functions
        /// <summary>
        /// When the directory button is clicked.
        /// </summary>
        private void directoryButtonClick()
        {
            // gets the files from the selected directory
            var files = Helpers.MediaFiles.GetFilesFromDirectory();

            // if we found no items in the directory, nothing to do
            if (!files.Any())
                return;

            // set Items list to new files list
            this.Items = this.Items = new ObservableCollection<Directory.ItemViewModel>(files.Select(file => new Directory.ItemViewModel(file)));

            // Check if this.Items list has anything, if so enable preview button
            if (this.Items.Any())
            {
                // if the list has items, we enable preview button
                this.enablePreviewButton();

                // and just make sure confirm button remains disabled
                this.disableConfirmButton();
            }
            else
            {
                // otherwise we make sure both buttons are disabled
                this.disablePreviewButton();
                this.disableConfirmButton();
            }
        }

        /// <summary>
        /// When the preview button is clicked.
        /// </summary>
        private void previewButtonClick()
        {
            //List<string> titles = Helpers.FetchTitles();

            // present changes to user
            //this.previewNewNames(titles);
            this.previewNewNames();

            // enable the confirm button
            this.enableConfirmButton();
        }

        /// <summary>
        /// When the confirm button is clicked.
        /// </summary>
        private void confirmButtonClick()
        {
            // make changes to files
            this.updateFileNames();

            // disable preview and confirm buttons
            this.disablePreviewButton();
            this.disableConfirmButton();
        }

        /// <summary>
        /// Retreives episode titles and presents a preview of the new names to the user.
        /// </summary>
        private void previewNewNames()
        {
            // update items with new names
            int counter = 1;
            foreach (var item in this.Items)
            {
                item.NewName = item.Name + counter.ToString();
                counter++;
            }
        }

        /// <summary>
        /// Changes file names to newly defined names.
        /// </summary>
        private void updateFileNames()
        {
            foreach (var item in this.Items)
            {
                System.IO.File.Move(item.FullPath, item.Directory + "\\" + item.NewName);
                item.FullPath = item.Directory + "\\" + item.NewName;
                item.Name = item.NewName;
                item.NewName = string.Empty;
            }
        }

        /// <summary>
        /// When the a Text Box receives focus, select all text in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxOnFocusSelectALlText(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;
            if (box != null && box.IsFocused)
                box.SelectAll();
        }

        /// <summary>
        /// Enables the preview button.
        /// </summary>
        private void enablePreviewButton()
        {
            // TODO
            // Recreating the object each time we enable or disable a button is a horrible
            // way to achieve this functionality. Fix it.
            this.PreviewButton = new ButtonViewModel(() => previewButtonClick(), true);
        }

        /// <summary>
        /// Disables the preview button.
        /// </summary>
        private void disablePreviewButton()
        {
            // TODO
            // Recreating the object each time we enable or disable a button is a horrible
            // way to achieve this functionality. Fix it.
            this.PreviewButton = new ButtonViewModel(null, false);
        }

        /// <summary>
        /// Enables just the confirm button.
        /// </summary>
        private void enableConfirmButton()
        {
            // TODO
            // Recreating the object each time we enable or disable a button is a horrible
            // way to achieve this functionality. Fix it.
            this.ConfirmButton = new ButtonViewModel(() => confirmButtonClick(), true);
        }

        /// <summary>
        /// Disables both preview and confirm buttons.
        /// </summary>
        private void disableConfirmButton()
        {
            // TODO
            // Recreating the object each time we enable or disable a button is a horrible
            // way to achieve this functionality. Fix it.
            this.ConfirmButton = new ButtonViewModel(null, false);
        }
        #endregion
    }
}
