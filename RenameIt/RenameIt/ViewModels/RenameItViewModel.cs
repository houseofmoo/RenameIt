using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    /// <summary>
    /// ViewModel for the main window.
    /// </summary>
    public class RenameItViewModel : Common.ViewModels.BaseViewModel
    {
        #region private constants
        private const string VISIBLE = "Visible";
        private const string HIDDEN = "Hidden";
        #endregion

        #region private fields
        // loading icon display property
        private bool _fetchingTitles = false;

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
        private string _videoListViewVisible = VISIBLE;
        private string _subtitleListViewVisible = VISIBLE;

        // data items
        private ObservableCollection<Directory.ItemViewModel> _videoItems;
        private ObservableCollection<Directory.ItemViewModel> _subtitleItems;

        // working directory
        private string _directoryPath;

        // sub page
        private Identifiers.Pages _currentSubPage;
        #endregion

        #region button properties
        /// <summary>
        /// Are we currently fetching episode titles, updated before and after 
        /// executing a fetch titles operation
        /// </summary>
        public bool FetchingTitles
        {
            get => _fetchingTitles;
            set => SetProperty(ref _fetchingTitles, value, nameof(FetchingTitles));
        }

        public ICommand DirectoryButtonCommand { get => _directroyButtonCommand; }
        public ICommand PreviewButtonCommand { get => _previewButtonCommand; }
        public ICommand ConfirmButtonCommand { get => _confirmButtonCommand; }
        public ICommand OptionsButtonCommand { get => _optionsButtonCommand; }
        public ICommand ExtensionsButtonCommand { get => _extensionsButtonCommand; }
        public ICommand VideoTitleCommand { get => _videoTitleCommand; }
        public ICommand SubtitleTitleCommand { get => _subtitleTitleCommand; }
        #endregion

        #region textbox properties
        /// <summary>
        /// The show name text box text
        /// </summary>
        public string ShowName
        {
            get => _showName;
            set => SetProperty(ref _showName, value);
        }

        /// <summary>
        /// The season text box text
        /// </summary>
        public string Season
        {
            get => _season;
            set => SetProperty(ref _season, value);
        }

        /// <summary>
        /// The episdoe start number text box text
        /// </summary>
        public string EpisodeStart
        {
            get => _episodeStart;
            set => SetProperty(ref _episodeStart, value);
        }
        #endregion

        #region formatting properties
        public string VideoListViewVisible
        {
            get => _videoListViewVisible;
            set => SetProperty(ref _videoListViewVisible, value);
        }

        public string SubtitleListViewVisible
        {
            get => _subtitleListViewVisible;
            set => SetProperty(ref _subtitleListViewVisible, value);
        }
        #endregion

        #region data items properties
        /// <summary>
        /// List of video items found in the directory
        /// </summary>
        public ObservableCollection<Directory.ItemViewModel> VideoItems
        {
            get => _videoItems;
            set => SetProperty(ref _videoItems, value);
        }

        /// <summary>
        /// List of subtitle items found in the directory
        /// </summary>
        public ObservableCollection<Directory.ItemViewModel> SubtitleItems
        {
            get => _subtitleItems;
            set => SetProperty(ref _subtitleItems, value);
        }
        #endregion

        #region subpages
        public Identifiers.Pages CurrentSubPage
        {
            get => _currentSubPage;
            set => SetProperty(ref _currentSubPage, value);
        }
        #endregion

        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public RenameItViewModel()
        {
            // create lists
            this.VideoItems = new ObservableCollection<Directory.ItemViewModel>();
            this.SubtitleItems = new ObservableCollection<Directory.ItemViewModel>();

            // buttom commands
            this._directroyButtonCommand = new Common.Commands.RelayCommand<object>(this.directoryButtonExecute);
            this._previewButtonCommand = new Common.Commands.RelayCommand<object>(this.previewButtonExecute);
            this._confirmButtonCommand = new Common.Commands.RelayCommand<object>(this.confirmButtonExecute);
            this._optionsButtonCommand = new Common.Commands.RelayCommand<object>(this.optionsButtonExecute);
            this._extensionsButtonCommand = new Common.Commands.RelayCommand<object>(this.extensionsButtonExecute);
            this._videoTitleCommand = new Common.Commands.RelayCommand<object>(this.videoTitleButtonExecute);
            this._subtitleTitleCommand = new Common.Commands.RelayCommand<object>(this.subtitleTitleButtonExecute);

            // current sub page
            this._currentSubPage = Identifiers.Pages.Options;
        }
        #endregion

        #region button click methods
        /// <summary>
        /// When the directory button is clicked.
        /// </summary>
        private void directoryButtonExecute(object obj)
        {
            // gets the files from the selected directory
            var files = Helpers.MediaFiles.GetFilesFromDirectory(out this._directoryPath);

            // build video files list
            var videoFiles = Helpers.MediaFiles.GetMatchingFiles(files, Properties.Settings.Default.VideoExtensions.Cast<string>().ToList());

            // build subtitle files list
            var subtitleFiles = Helpers.MediaFiles.GetMatchingFiles(files, Properties.Settings.Default.SubtitleExtensions.Cast<string>().ToList());

            // if we found no valid items in the directory
            if (!videoFiles.Any() && !subtitleFiles.Any())
            {
                this.VideoItems = new ObservableCollection<Directory.ItemViewModel>();
                this.SubtitleItems = new ObservableCollection<Directory.ItemViewModel>();
                return;
            }

            // build and set VideoItems list
            this.VideoItems = new ObservableCollection<Directory.ItemViewModel>(videoFiles.Select(file => new Directory.ItemViewModel(file)));

            // do we want subtitle files?
            if (Properties.Settings.Default.IncludeSubtitles)
                this.SubtitleItems = new ObservableCollection<Directory.ItemViewModel>(subtitleFiles.Select(file => new Directory.ItemViewModel(file)));
        }

        /// <summary>
        /// When the preview button is clicked.
        /// </summary>
        private async void previewButtonExecute(object obj)
        {
            // if input is invalid and we have video or subtitle items to update
            if (Helpers.Util.InvalidTextBoxInput(this.ShowName, this.Season, this.EpisodeStart) ||
                (!this.VideoItems.Any() || !this.SubtitleItems.Any()))
            {
                MessageBox.Show("Please provide a show name, an season number (decimal) and a episode begin number (decimal).");
                return;
            }

            // only fetch titles if user wants to
            if (Properties.Settings.Default.GetEpisodeTitles)
            {
                // run command if we're not already running command
                await RunCommand(() => this.FetchingTitles, async () =>
                {
                    // create request
                    var showInfo = new Models.Titles.Request()
                    {
                        ShowName = this.ShowName,
                        Season = this.Season,
                        EpisodeBegin = this.EpisodeStart,
                        EpisodeCount = this.VideoItems.Count,
                    };
                    // get titles list
                    var titles = await Helpers.Fetcher.GetEpisodeTitles(showInfo);

                    // present changes to user
                    this.previewNewNames(titles);
                });
            }
            else
            {
                // present changes to user
                this.previewNewNames(null);
            }

        }

        /// <summary>
        /// When the confirm button is clicked.
        /// </summary>
        private void confirmButtonExecute(object obj)
        {
            // if input is invalid and we have video or subtitle items to update
            if (Helpers.Util.InvalidTextBoxInput(this.ShowName, this.Season, this.EpisodeStart) ||
                (!this.VideoItems.Any() || !this.SubtitleItems.Any()))
            {
                MessageBox.Show("Please provide a show name, an season number (decimal) and a episode begin number (decimal).");
                return;
            }

            // make changes to files
            Helpers.MediaFiles.UpdateFileNames(this.VideoItems);
            Helpers.MediaFiles.UpdateFileNames(this.SubtitleItems);

            // delete non-media files
            if (Properties.Settings.Default.DeleteNonMediaFiles)
                Helpers.MediaFiles.DeleteInvalidFiles(this._directoryPath);
        }

        /// <summary>
        /// Changes to the options subpage
        /// </summary>
        private void optionsButtonExecute(object obj)
        {
            if (this.CurrentSubPage != Identifiers.Pages.Options)
            {
                this.CurrentSubPage = Identifiers.Pages.Options;
            }
        }

        /// <summary>
        /// Changes to the extensions subpage
        /// </summary>
        private void extensionsButtonExecute(object obj)
        {
            if (this.CurrentSubPage != Identifiers.Pages.Extensions)
            {
                this.CurrentSubPage = Identifiers.Pages.Extensions;
            }
            
        }

        /// <summary>
        /// Hides the video list box
        /// </summary>
        private void videoTitleButtonExecute(object obj)
        {
            this.VideoListViewVisible = (this.VideoListViewVisible == HIDDEN) ? VISIBLE : HIDDEN;
        }

        /// <summary>
        /// Hides the subtitle list box
        /// </summary>
        private void subtitleTitleButtonExecute(object obj)
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
