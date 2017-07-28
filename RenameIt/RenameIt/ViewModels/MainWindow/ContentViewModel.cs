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
        // text box default content values
        private const string SHOWNAME = "Show Name";
        private const string SEASON = "Season";
        private const string EPISODESTART = "Episode Start Number";

        // button text content values
        private const string DIRECTORY = "Select Directory";
        private const string PREVIEW = "Preview";
        private const string CONFIRM = "Confirm";

        // check box content values
        private const string GET_EPISODE_TITLES = "Get Episode Titles";
        private const string IGNORE_SUBTITLES = "Ignore Subtitles";
        private const string DELETE_NONMEDIA_FILES = "Delete Non-media Files";
        private const string SEARCH_SUBDIRECTORIES = "Search Sub-directories";

        // file types

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
        private ObservableCollection<Directory.VideoItemViewModel> _videoItems;

        // formatting
        private double _listViewColumnSize = 265f;

        // options
        private bool _getEpisodeTitles = true;
        private bool _ignoreSubtitles = false;
        private bool _deleteNonMediaFiles = false;
        private bool _searchSubDirectories = false;
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
                _showName = value == string.Empty ? SHOWNAME : value;
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
        public ObservableCollection<Directory.VideoItemViewModel> VideoItems
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
        public bool IgnoreSubtitles
        {
            get { return _ignoreSubtitles; }
            set
            {
                if (_ignoreSubtitles == value)
                    return;
                _ignoreSubtitles = value;
                OnPropertyChanged(nameof(IgnoreSubtitles));
            }
        }

        /// <summary>
        /// Returns the content for Ignore Subtitles checkbox
        /// </summary>
        public string IgnoreSubtitlesContent { get { return IGNORE_SUBTITLES; } }

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

        #region constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContentViewModel()
        {
            // create items data structure, this holds the bulk of the data
            this.VideoItems = new ObservableCollection<Directory.VideoItemViewModel>();

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

            // get the files that are actually video files
            if (this.IgnoreSubtitles)
                files = Helpers.MediaFiles.GetOnlyVideoFiles(files);
            else
                files = Helpers.MediaFiles.GetVideoAndSubtitleFiles(files);

            // if we found no items in the directory, nothing to do
            if (!files.Any())
                return;

            // set Items list to new files list
            this.VideoItems = new ObservableCollection<Directory.VideoItemViewModel>(files.Select(file => new Directory.VideoItemViewModel(file)));

            // if items list has any items, we enable preview button
            if (this.VideoItems.Any())
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
            Helpers.MediaFiles.UpdateFileNames(this.VideoItems);

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
        }
        #endregion
    }
}
