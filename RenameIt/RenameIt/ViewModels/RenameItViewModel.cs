using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    /// <summary>
    /// ViewModel for the main window.
    /// </summary>
    class RenameItViewModel : Base.ViewModel
    {
        #region private constants
        // button text content values
        private const string DIRECTORY = "Select Directory";
        private const string PREVIEW = "Preview Changes";
        private const string CONFIRM = "Commit Changes";

        // formatting constants
        private const double LIST_VIEW_COLUMN_SIZE = 205d;
        private const double MINIMUM_LIST_VIEW_HEIGHT = 335d;
        #endregion

        #region private fields
        // button commands
        private ICommand _directroyButtonCommand;
        private ICommand _previewButtonCommand;
        private ICommand _confirmButtonCommand;

        // button enabled
        private bool _directoryButtonEnabled = true;
        private bool _previewButtonEnabled = false;
        private bool _confirmButtonEnabled = false;

        // textbox text
        private string _showName;
        private string _season;
        private string _episodeStart;

        // formatting
        private double _listViewColumnSize = LIST_VIEW_COLUMN_SIZE;
        private double _listViewHeight = MINIMUM_LIST_VIEW_HEIGHT;

        // data items
        private ObservableCollection<Directory.ItemViewModel> _videoItems;
        private ObservableCollection<Directory.ItemViewModel> _subtitleItems;

        // working directory
        private string _directoryPath;
        #endregion

        #region button properties
        /// <summary>
        /// Get directory button command
        /// </summary>
        public ICommand DirectoryButtonCommand { get { return _directroyButtonCommand; } }

        /// <summary>
        /// Returns directory button content.
        /// </summary>
        public string DirectoryButtonContent { get { return DIRECTORY; } }

        /// <summary>
        /// Get preview button command
        /// </summary>
        public ICommand PreviewButtonCommand { get { return _previewButtonCommand; } }

        /// <summary>
        /// Returns preview button content
        /// </summary>
        public string PreviewButtonContent { get { return PREVIEW; } }

        /// <summary>
        /// Gets confirm button command
        /// </summary>
        public ICommand ConfirmButtonCommand { get { return _confirmButtonCommand; } }

        /// <summary>
        /// Returns confirm button content.
        /// </summary>
        public string ConfirmButtonContent { get { return CONFIRM; } }

        /// <summary>
        /// Enables and disables the directory button
        /// </summary>
        public bool DirectoryButtonEnabled
        {
            get { return _directoryButtonEnabled; }
            set
            {
                if (_directoryButtonEnabled == value)
                    return;
                _directoryButtonEnabled = value;
                OnPropertyChanged(nameof(DirectoryButtonEnabled));
            }
        }

        /// <summary>
        /// Enables and disables the preview button
        /// </summary>
        public bool PreviewButtonEnabled
        {
            get { return _previewButtonEnabled; }
            set
            {
                if (_previewButtonEnabled == value)
                    return;
                _previewButtonEnabled = value;
                OnPropertyChanged(nameof(PreviewButtonEnabled));
            }
        }

        /// <summary>
        /// Enables and disables the confirm button
        /// </summary>
        public bool ConfirmButtonEnabled
        {
            get { return _confirmButtonEnabled; }
            set
            {
                if (_confirmButtonEnabled == value)
                    return;
                _confirmButtonEnabled = value;
                OnPropertyChanged(nameof(ConfirmButtonEnabled));
            }
        }
        #endregion

        #region textbox properties
        /// <summary>
        /// The show name text box text
        /// </summary>
        public string ShowName
        {
            get { return _showName; }
            set
            {
                if (value == _showName)
                    return;
                _showName = value;
                OnPropertyChanged(nameof(ShowName));
            }
        }

        /// <summary>
        /// The season text box text
        /// </summary>
        public string Season
        {
            get { return _season; }
            set
            {
                if (value == _season)
                    return;
                _season = value;
                OnPropertyChanged(nameof(Season));
            }
        }

        /// <summary>
        /// The episdoe start number text box text
        /// </summary>
        public string EpisodeStart
        {
            get { return _episodeStart; }
            set
            {
                if (value == _episodeStart)
                    return;
                _episodeStart = value;
                OnPropertyChanged(nameof(EpisodeStart));
            }
        }
        #endregion

        #region formatting properties
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

        /// <summary>
        /// Holds the list view height. Sets a default valut. Value cannot go below default value
        /// </summary>
        public double ListViewHeight
        {
            get { return _listViewHeight; }
            set
            {
                if (_listViewHeight == value)
                    return;
                _listViewHeight = value < MINIMUM_LIST_VIEW_HEIGHT ? MINIMUM_LIST_VIEW_HEIGHT : value;
                OnPropertyChanged(nameof(ListViewHeight));
            }
        }
        #endregion

        #region data items properties
        /// <summary>
        /// List of video items found in the directory
        /// </summary>
        public ObservableCollection<Directory.ItemViewModel> VideoItems
        {
            get { return _videoItems; }
            set
            {
                if (_videoItems == value)
                    return;
                _videoItems = value;
                OnPropertyChanged(nameof(VideoItems));
            }
        }

        /// <summary>
        /// List of subtitle items found in the directory
        /// </summary>
        public ObservableCollection<Directory.ItemViewModel> SubtitleItems
        {
            get { return _subtitleItems; }
            set
            {
                if (_subtitleItems == value)
                    return;
                _subtitleItems = value;
                OnPropertyChanged(nameof(SubtitleItems));
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RenameItViewModel()
        {
            // create lists
            this.VideoItems = new ObservableCollection<Directory.ItemViewModel>();
            this.SubtitleItems = new ObservableCollection<Directory.ItemViewModel>();

            // pass mutator functions to button view model which contain the command
            this._directroyButtonCommand = new Commands.RelayCommand(this.directoryButtonClick, true);
            this._previewButtonCommand = new Commands.RelayCommand(this.previewButtonClick, true);
            this._confirmButtonCommand = new Commands.RelayCommand(this.confirmButtonClick, true);
        }
        #endregion

        #region button click methods
        /// <summary>
        /// When the directory button is clicked.
        /// </summary>
        private void directoryButtonClick()
        {
            // disable confirm button, we do not want users to be able to
            // confirm changes before previewing them
            this.ConfirmButtonEnabled = false;

            // gets the files from the selected directory
            var files = Helpers.MediaFiles.GetFilesFromDirectory(out this._directoryPath);

            // build video files list
            var videoFiles = Helpers.MediaFiles.GetMatchingFiles(files, User.Settings.VideoExtensions);

            // build subtitle files list
            var subtitleFiles = Helpers.MediaFiles.GetMatchingFiles(files, User.Settings.SubtitleExtensions);

            // if we found no valid items in the directory, nothing to do
            if (!videoFiles.Any() && !subtitleFiles.Any())
                return;

            // build and set VideoItems list
            this.VideoItems = new ObservableCollection<Directory.ItemViewModel>(videoFiles.Select(file => new Directory.ItemViewModel(file)));

            // do we want subtitle files?
            if (User.Settings.IncludeSubtitles)
                this.SubtitleItems = new ObservableCollection<Directory.ItemViewModel>(subtitleFiles.Select(file => new Directory.ItemViewModel(file)));

            // if items lists have any items, we enable preview button
            if (this.VideoItems.Any() || this.SubtitleItems.Any())
                this.PreviewButtonEnabled = true;
        }

        /// <summary>
        /// When the preview button is clicked.
        /// </summary>
        private void previewButtonClick()
        {
            // if input is invalid
            if (!Helpers.Util.ValidateInput(this.ShowName, this.Season, this.EpisodeStart, this.VideoItems.Count))
            {
                MessageBox.Show("Please provide a show name, an season number (decimal) and a episode begin number (decimal).");
                return;
            }

            // list to hold titles
            List<string> titles = null;

            // only fetch titles if user wants to
            if (User.Settings.GetEpisodeTitles)
            {
                // create request
                var showInfo = new Models.Titles.Request()
                {
                    ShowName = this.ShowName,
                    Season = this.Season,
                    EpisodeBegin = this.EpisodeStart,
                    EpisodeCount = this.VideoItems.Count,
                };

                // fetch titles
                titles = Helpers.Fetcher.GetEpisodeTitles(showInfo);
            }

            // present changes to user
            this.previewNewNames(titles);

            // enable the confirm button
            this.ConfirmButtonEnabled = true;
        }

        /// <summary>
        /// When the confirm button is clicked.
        /// </summary>
        private void confirmButtonClick()
        {
            // make changes to files
            Helpers.MediaFiles.UpdateFileNames(this.VideoItems);
            Helpers.MediaFiles.UpdateFileNames(this.SubtitleItems);

            // disable confirm button
            this.ConfirmButtonEnabled = false;

            // if for some reason we have confirmed on no items, we disable preview button
            if (!this.VideoItems.Any() && !this.SubtitleItems.Any())
                this.PreviewButtonEnabled = false;
        }
        #endregion

        #region misc methods
        /// <summary>
        /// Update items with their new name, for presentation purposes only. This does not change the name of the file
        /// </summary>
        private void previewNewNames(List<string> titles)
        {
            // counter to increment episode number
            int episodeNumber = Convert.ToInt32(this.EpisodeStart);

            // index to episode title
            int titleIndex = 0;

            // holds episode title
            string title;

            // update video items with new names
            foreach (var item in this.VideoItems)
            {
                // check if we can add the title
                if (titles != null && titleIndex < titles.Count)
                    title = titles[titleIndex];
                else
                    title = string.Empty;

                // build new name and add it to the items field
                item.SetNewName(this.ShowName, this.Season, episodeNumber, title);

                // increment counter and indexer
                episodeNumber++;
                titleIndex++;
            }

            // reset for subtitles
            episodeNumber = Convert.ToInt32(this.EpisodeStart);
            titleIndex = 0;

            // update subtitle items with new names
            foreach (var item in this.SubtitleItems)
            {
                // check if we can add the title
                if (titles != null && titleIndex < titles.Count)
                    title = titles[titleIndex];
                else
                    title = string.Empty;

                // build new name and add it to the items field
                item.SetNewName(this.ShowName, this.Season, episodeNumber, title);

                // increment counter and indexer
                episodeNumber++;
                titleIndex++;
            }
        }
        #endregion
    }
}
