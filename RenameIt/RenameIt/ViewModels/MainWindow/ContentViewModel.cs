using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RenameIt.ViewModels.MainWindow
{
    /// <summary>
    /// ViewModel for the main window.
    /// </summary>
    class ContentViewModel : Base.ViewModel
    {
        #region private constants
        // text box default values
        private const string SHOWNAME = "Show Name";
        private const string SEASON = "Season";
        private const string EPISODESTART = "Episode Start Number";

        // button text default values
        private const string DIRECTORY = "Select Directory";
        private const string PREVIEW = "Preview";
        private const string CONFIRM = "Confirm";
        #endregion

        #region private fields
        // buttons
        private ButtonViewModel _directoryButton;
        private ButtonViewModel _previewButton;
        private ButtonViewModel _confirmButton;

        // text boxes text
        private string _showName = SHOWNAME;
        private string _season = SEASON;
        private string _episodeStart = EPISODESTART;

        // data
        private ObservableCollection<Directory.ItemViewModel> _items;

        // formatting
        private double _listViewColumnSize = 265f;

        // options
        private bool _getEpisodeTitles = true;
        private bool _ignoreSubTitles = false;
        private bool _deleteNonMediaFiles = true;
        private bool _searchSubDirectories = true;
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
                if (_directoryButton == value)
                    return;
                _directoryButton = value;
                OnPropertyChanged(nameof(DirectoryButton));
            }
        }

        /// <summary>
        /// Returns directory button content.
        /// </summary>
        public string DirectoryButtonContent { get { return DIRECTORY; } }

        /// <summary>
        /// Preview button.
        /// </summary>
        public ButtonViewModel PreviewButton
        {
            get { return _previewButton; }
            set
            {
                if (_previewButton == value)
                    return;
                _previewButton = value;
                OnPropertyChanged(nameof(PreviewButton));
            }
        }

        /// <summary>
        /// Returns preview button content
        /// </summary>
        public string PreviewButtonContent { get { return PREVIEW; } }

        /// <summary>
        /// Confirm button.
        /// </summary>
        public ButtonViewModel ConfirmButton
        {
            get { return _confirmButton; }
            set
            {
                if (_confirmButton == value)
                    return;
                _confirmButton = value;
                OnPropertyChanged(nameof(ConfirmButton));
            }
        }

        /// <summary>
        /// Returns confirm button content.
        /// </summary>
        public string ConfirmButtonContent { get { return CONFIRM; } }

        /// <summary>
        /// The show name text boxes text. If text is ever empty, sets value to "Show Name".
        /// </summary>
        public string ShowName
        {
            get { return _showName; }
            set
            {
                if (value == _showName)
                    return;
                _showName = value == string.Empty ? ShowName : value;
                OnPropertyChanged(nameof(ShowName));
            }
        }

        /// <summary>
        /// The season text boxes text. If text is ever empty, sets value to "Season".
        /// </summary>
        public string Season
        {
            get { return _season; }
            set
            {
                if (value == _season)
                    return;
                _season = value == string.Empty ? SEASON : value;
                OnPropertyChanged(nameof(Season));
            }
        }

        /// <summary>
        /// The episdoe start number text boxes text. If text is ever empty, sets value to "Episode Start Number".
        /// </summary>
        public string EpisodeStart
        {
            get { return _episodeStart; }
            set
            {
                if (value == _episodeStart)
                    return;
                _episodeStart = value == string.Empty ? EPISODESTART : value;
                OnPropertyChanged(nameof(EpisodeStart));
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
                if (_items == value)
                    return;
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        /// <summary>
        /// Sets the column width of the list view initially
        /// </summary>
        public double ListViewColumnSize
        {
            get { return _listViewColumnSize; }
            set
            {
                if (_listViewColumnSize == value)
                    return;
                _listViewColumnSize = value;
                OnPropertyChanged(nameof(ListViewColumnSize));
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContentViewModel()
        {
            // create items data structure, this holds the bulk of the data
            this.Items = new ObservableCollection<Directory.ItemViewModel>();

            // pass mutator functions to button view model which contain the commands
            this.DirectoryButton = new ButtonViewModel(() => directoryButtonClick(), true);
            this.PreviewButton = new ButtonViewModel(null, false);
            this.ConfirmButton = new ButtonViewModel(null, false);
        }
        #endregion

        #region button click methods
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
            this.Items = new ObservableCollection<Directory.ItemViewModel>(files.Select(file => new Directory.ItemViewModel(file)));

            // if items list has any items, we enable preview button
            if (this.Items.Any())
            {
                // TODO
                // Recreating the object each time we enable or disable a button is a horrible
                // way to achieve this functionality. Fix it.
                this.PreviewButton = new ButtonViewModel(() => previewButtonClick(), true);
            }
        }

        /// <summary>
        /// When the preview button is clicked.
        /// </summary>
        private void previewButtonClick()
        {
            // if input is invalid
            if (!Helpers.Util.ValidateInput(this.ShowName, this.Season, this.EpisodeStart, this.Items.Count))
            {
                MessageBox.Show("Please provide a show name, an season number (decimal) and a episode begin number (decimal).");
                return;
            }

            // create request
            var showInfo = new Models.Titles.Request()
            {
                ShowName = this.ShowName,
                Season = this.Season,
                EpisodeBegin = this.EpisodeStart,
                EpisodeCount = this.Items.Count,
            };

            // fetch titles
            List<string> titles = Helpers.Titles.Fetcher.GetEpisodeTitles(showInfo);

            // present changes to user
            this.previewNewNames(titles);

            // enable the confirm button
            // TODO
            // Recreating the object each time we enable or disable a button is a horrible
            // way to achieve this functionality. Fix it.
            this.ConfirmButton = new ButtonViewModel(() => confirmButtonClick(), true);
        }

        /// <summary>
        /// When the confirm button is clicked.
        /// </summary>
        private void confirmButtonClick()
        {
            // make changes to files
            Helpers.MediaFiles.UpdateFileNames(this.Items);

            // disable confirm button
            // TODO
            // Recreating the object each time we enable or disable a button is a horrible
            // way to achieve this functionality. Fix it.
            this.ConfirmButton = new ButtonViewModel(null, false);
        }
        #endregion

        #region misc methods
        /// <summary>
        /// Retreives episode titles and presents a preview of the new names to the user.
        /// </summary>
        private void previewNewNames(List<string> titles)
        {
            // counter to increment episode number
            int counter = Convert.ToInt32(this.EpisodeStart);

            // index to titles
            int titleIndex = 0;
            var title = string.Empty;

            // update items with new names
            foreach (var item in this.Items)
            {
                // check if we can add the title
                if (titles != null && titleIndex < titles.Count)
                {
                    title = titles[titleIndex];
                }
                else
                {
                    title = string.Empty;
                }

                // build new name and add it to the items field
                item.AddNewName(this.ShowName, this.Season, counter, title);

                // increment counter and indexer
                counter++;
                titleIndex++;
            }
        }
        #endregion
    }
}
