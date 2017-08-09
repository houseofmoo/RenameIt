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
        private const string EXTENSTIONS = "Extensions";
        private const string OPTIONS = "Options";
        private const string VIDEO_TITLE = "Videos";
        private const string SUBTITLE_TITLE = "Subtitles";

        // formatting constants
        private const double LIST_VIEW_COLUMN_SIZE = 205d;
        private const double MINIMUM_LIST_VIEW_HEIGHT = 335d;

        private const string VISIBLE = "Visible";
        private const string HIDDEN = "Hidden";
        #endregion

        #region private fields
        // button commands
        private ICommand _directroyButtonCommand;
        private ICommand _previewButtonCommand;
        private ICommand _confirmButtonCommand;
        private ICommand _optionsButtonCommand;
        private ICommand _extensionsButtonCommand;
        private ICommand _videoTitleCommand;
        private ICommand _subtitleTitleCommand;

        // textbox text
        private string _showName;
        private string _season;
        private string _episodeStart;

        // formatting
        private double _listViewColumnSize = LIST_VIEW_COLUMN_SIZE;
        private string _videoListBoxVisible = VISIBLE;
        private string _subtitleListBoxVisible = VISIBLE;

        // data items
        private ObservableCollection<Directory.ItemViewModel> _videoItems;
        private ObservableCollection<Directory.ItemViewModel> _subtitleItems;

        // working directory
        private string _directoryPath;

        // sub page
        private Identifiers.Pages _currentSubPage;
        #endregion

        #region button properties
        public string DirectoryButtonContent { get { return DIRECTORY; } }
        public string PreviewButtonContent { get { return PREVIEW; } }
        public string ConfirmButtonContent { get { return CONFIRM; } }
        public string OptionsButtonContent { get { return OPTIONS; } }
        public string ExtensionsButtonContent { get { return EXTENSTIONS; } }
        public string VideoTitleContent { get { return VIDEO_TITLE; } }
        public string SubtitleTitleContent { get { return SUBTITLE_TITLE; } }

        public ICommand DirectoryButtonCommand { get { return _directroyButtonCommand; } }
        public ICommand PreviewButtonCommand { get { return _previewButtonCommand; } }
        public ICommand ConfirmButtonCommand { get { return _confirmButtonCommand; } }
        public ICommand OptionsButtonCommand { get { return _optionsButtonCommand; } }
        public ICommand ExtensionsButtonCommand { get { return _extensionsButtonCommand; } }
        public ICommand VideoTitleCommand { get { return _videoTitleCommand; } }
        public ICommand SubtitleTitleCommand { get { return _subtitleTitleCommand; } }
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
        public double ListViewHeight { get { return MINIMUM_LIST_VIEW_HEIGHT; } }

        public string VideoListViewVisible
        {
            get { return _videoListBoxVisible; }
            set
            {
                if (_videoListBoxVisible == value)
                    return;
                _videoListBoxVisible = value;
                OnPropertyChanged(nameof(VideoListViewVisible));
            }
        }

        public string SubtitleListViewVisible
        {
            get { return _subtitleListBoxVisible; }
            set
            {
                if (_subtitleListBoxVisible == value)
                    return;
                _subtitleListBoxVisible = value;
                OnPropertyChanged(nameof(SubtitleListViewVisible));
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

        #region subpages
        public Identifiers.Pages CurrentSubPage
        {
            get { return _currentSubPage; }
            set
            {
                if (_currentSubPage == value)
                    return;
                _currentSubPage = value;
                OnPropertyChanged(nameof(CurrentSubPage));
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
            this._optionsButtonCommand = new Commands.RelayCommand(this.optionsButtonClick, true);
            this._extensionsButtonCommand = new Commands.RelayCommand(this.extensionsButtonClick, true);
            this._videoTitleCommand = new Commands.RelayCommand(this.videoTitleButtonClick, true);
            this._subtitleTitleCommand = new Commands.RelayCommand(this.subtitleTitleButtonClick, true);

            // current sub page
            this._currentSubPage = Identifiers.Pages.Options;
        }
        #endregion

        #region button click methods
        /// <summary>
        /// When the directory button is clicked.
        /// </summary>
        private void directoryButtonClick()
        {
            // gets the files from the selected directory
            var files = Helpers.MediaFiles.GetFilesFromDirectory(out this._directoryPath);

            // build video files list
            var videoFiles = Helpers.MediaFiles.GetMatchingFiles(files, Identifiers.Extensions.VideoDefaults.ToList());

            // build subtitle files list
            var subtitleFiles = Helpers.MediaFiles.GetMatchingFiles(files, Identifiers.Extensions.SubtitleDefaults.ToList());

            // if we found no valid items in the directory, nothing to do
            if (!videoFiles.Any() && !subtitleFiles.Any())
                return;

            // build and set VideoItems list
            this.VideoItems = new ObservableCollection<Directory.ItemViewModel>(videoFiles.Select(file => new Directory.ItemViewModel(file)));

            // do we want subtitle files?
            if (Properties.Settings.Default.IncludeSubtitles)
                this.SubtitleItems = new ObservableCollection<Directory.ItemViewModel>(subtitleFiles.Select(file => new Directory.ItemViewModel(file)));
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
            if (Properties.Settings.Default.GetEpisodeTitles)
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
        }

        /// <summary>
        /// When the confirm button is clicked.
        /// </summary>
        private void confirmButtonClick()
        {
            // make changes to files
            Helpers.MediaFiles.UpdateFileNames(this.VideoItems);
            Helpers.MediaFiles.UpdateFileNames(this.SubtitleItems);

            // delete non-media files
            if (Properties.Settings.Default.DeleteNonMediaFiles)
            {
                // get files from dir
                // check extensions again sub/vid
                // delete ones that don't match
            }
        }

        /// <summary>
        /// Changes to the options subpage
        /// </summary>
        private void optionsButtonClick()
        {
            this.CurrentSubPage = Identifiers.Pages.Options;
        }

        /// <summary>
        /// Changes to the extensions subpage
        /// </summary>
        private void extensionsButtonClick()
        {
            this.CurrentSubPage = Identifiers.Pages.Extensions;
        }

        /// <summary>
        /// Hides the video list box
        /// </summary>
        private void videoTitleButtonClick()
        {
            this.VideoListViewVisible = (this.VideoListViewVisible == HIDDEN) ? VISIBLE : HIDDEN;
        }

        /// <summary>
        /// Hides the subtitle list box
        /// </summary>
        private void subtitleTitleButtonClick()
        {
            this.SubtitleListViewVisible = (this.SubtitleListViewVisible == HIDDEN) ? VISIBLE : HIDDEN;
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
