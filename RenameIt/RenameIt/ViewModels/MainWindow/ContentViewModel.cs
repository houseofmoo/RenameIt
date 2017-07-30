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
        // button text content values
        private const string DIRECTORY = "Select Directory";
        private const string PREVIEW = "Preview";
        private const string CONFIRM = "Confirm";

        // check box content values
        private const string GET_EPISODE_TITLES = "Get Episode Titles";
        private const string INCLUDE_SUBTITLES = "Include Subtitles";
        private const string DELETE_NONMEDIA_FILES = "Delete Non-media Files";
        private const string SEARCH_SUBDIRECTORIES = "Search Sub-directories";

        // formatting constants
        private const double LIST_VIEW_COLUMN_SIZE = 205d;
        private const double MINIMUM_LIST_VIEW_HEIGHT = 335d;
        #endregion

        #region private fields
        // buttons
        private ButtonViewModel _directoryButton;
        private ButtonViewModel _previewButton;
        private ButtonViewModel _confirmButton;

        // button enabled
        private bool _directoryButtonEnabled = true;
        private bool _previewButtonEnabled = false;
        private bool _confirmButtonEnabled = false;

        // textbox text
        private string _showName;
        private string _season;
        private string _episodeStart;

        // checkbox
        private bool _getEpisodeTitles = true;
        private bool _includeSubtitles = true;
        private bool _deleteNonMediaFiles = false;
        private bool _searchSubDirectories = false;

        // formatting
        private double _listViewColumnSize = LIST_VIEW_COLUMN_SIZE;
        private double _listViewHeight = MINIMUM_LIST_VIEW_HEIGHT;

        // data items
        private ObservableCollection<Directory.ItemViewModel> _videoItems;
        private ObservableCollection<Directory.ItemViewModel> _subtitleItems;
        #endregion

        #region button properties
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

        #region checkbox properties
        /// <summary>
        /// Stores the checkbox value of Get Episode Titles
        /// </summary>
        public bool GetEpisodeTitles
        {
            get { return _getEpisodeTitles; }
            set
            {
                if (_getEpisodeTitles == value)
                    return;
                _getEpisodeTitles = value;
                OnPropertyChanged(nameof(GetEpisodeTitles));
            }
        }

        /// <summary>
        /// Returns the content for Get Episode Titles checkbox
        /// </summary>
        public string GetEpisodeTitlesContent { get { return GET_EPISODE_TITLES; } }

        /// <summary>
        /// Stores the checkbox value of Ignore Subtitles
        /// </summary>
        public bool IncludeSubtitles
        {
            get { return _includeSubtitles; }
            set
            {
                if (_includeSubtitles == value)
                    return;
                _includeSubtitles = value;
                OnPropertyChanged(nameof(IncludeSubtitles));
            }
        }

        /// <summary>
        /// Returns the content for Ignore Subtitles checkbox
        /// </summary>
        public string IncludeSubtitlesContent { get { return INCLUDE_SUBTITLES; } }

        /// <summary>
        /// Stores the checkbox value of Delete Non-media Files
        /// </summary>
        public bool DeleteNonMediaFiles
        {
            get { return _deleteNonMediaFiles; }
            set
            {
                if (_deleteNonMediaFiles == value)
                    return;
                _deleteNonMediaFiles = value;
                OnPropertyChanged(nameof(DeleteNonMediaFiles));
            }
        }

        /// <summary>
        /// Returns the content for Delete Non-media Files checkbox
        /// </summary>
        public string DeleteNonMediaFilesContent { get { return DELETE_NONMEDIA_FILES; } }

        /// <summary>
        /// Stores the checkbox value of Search Subdirectories
        /// </summary>
        public bool SearchSubDirectories
        {
            get { return _searchSubDirectories; }
            set
            {
                if (_searchSubDirectories == value)
                    return;
                _searchSubDirectories = value;
                OnPropertyChanged(nameof(SearchSubDirectories));
            }
        }

        /// <summary>
        /// Returns the content for Search Subdirectories checkbox
        /// </summary>
        public string SearchSubDirectoriesContent { get { return SEARCH_SUBDIRECTORIES; } }
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
        public ContentViewModel()
        {
            // pass mutator functions to button view model which contain the command
            this.DirectoryButton = new ButtonViewModel(() => directoryButtonClick(), true);
            this.PreviewButton = new ButtonViewModel(() => previewButtonClick(), true);
            this.ConfirmButton = new ButtonViewModel(() => confirmButtonClick(), true);
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
            List<Models.DirectoryItem> videoFiles = Helpers.MediaFiles.GetMatchingFiles(files, Identifiers.Extensions.Video);
            List<Models.DirectoryItem> subtitleFiles = Helpers.MediaFiles.GetMatchingFiles(files, Identifiers.Extensions.Subtitle);

            // if we found no valid items in the directory, nothing to do
            if (!videoFiles.Any() && !subtitleFiles.Any())
                return;

            // set Items list to new files list
            this.VideoItems = new ObservableCollection<Directory.ItemViewModel>(videoFiles.Select(file => new Directory.ItemViewModel(file)));

            // do we want subtitle files?
            if (this.IncludeSubtitles)
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
            if (this.GetEpisodeTitles)
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
                titles = Helpers.Titles.Fetcher.GetEpisodeTitles(showInfo);
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
            int counter = Convert.ToInt32(this.EpisodeStart);

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
                item.AddNewName(this.ShowName, this.Season, counter, title);

                // increment counter and indexer
                counter++;
                titleIndex++;
            }

            // reset for subtitles
            counter = Convert.ToInt32(this.EpisodeStart);
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
                item.AddNewName(this.ShowName, this.Season, counter, title);

                // increment counter and indexer
                counter++;
                titleIndex++;
            }
        }
        #endregion
    }
}
