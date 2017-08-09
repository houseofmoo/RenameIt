﻿using System.Windows.Input;

namespace RenameIt.ViewModels
{
    class OptionsViewModel : Base.ViewModel
    {
        #region private constants
        // check box content values
        private const string GET_EPISODE_TITLES = "Get Episode Titles";
        private const string INCLUDE_SUBTITLES = "Include Subtitles";
        private const string DELETE_NONMEDIA_FILES = "Delete Non-Media Files";
        private const string SEARCH_SUBDIRECTORIES = "Search Sub-directories";
        private const string APPLY = "Apply Changes";
        private const string CANCEL = "Cancel";
        #endregion

        #region private fields
        /// <summary>
        /// Holds the settings as of page load. Used if user cancels their changes
        /// </summary>
        private User.Settings _previousState;

        // check box values
        private bool _getEpisodeTitles = User.Settings.Get().GetEpisodeTitles;
        private bool _includeSubtitles = User.Settings.Get().IncludeSubtitles;
        private bool _deleteNonMediaFiles = User.Settings.Get().DeleteNonMediaFiles;
        private bool _searchSubdirectories = User.Settings.Get().SearchSubDirectories;

        // commands
        private ICommand _applyButtonCommand;
        private ICommand _cancelButtonCommand;
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
        /// Stores the checkbox value of Search Subdirectories
        /// </summary>
        public bool SearchSubDirectories
        {
            get { return _searchSubdirectories; }
            set
            {
                if (_searchSubdirectories == value)
                    return;
                _searchSubdirectories = value;
                OnPropertyChanged(nameof(SearchSubDirectories));
            }
        }

        public string GetEpisodeTitlesContent { get { return GET_EPISODE_TITLES; } }
        public string IncludeSubtitlesContent { get { return INCLUDE_SUBTITLES; } }
        public string DeleteNonMediaFilesContent { get { return DELETE_NONMEDIA_FILES; } }
        public string SearchSubDirectoriesContent { get { return SEARCH_SUBDIRECTORIES; } }
        #endregion

        #region button properties
        public string ApplyButtonContent { get { return APPLY; } }
        public string CancelButtonContent { get { return CANCEL; } }

        public ICommand ApplyButtonCommand { get { return _applyButtonCommand; } }
        public ICommand CancelButtonCommand { get { return _cancelButtonCommand; } }
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public OptionsViewModel()
        {
            // hold the previous state
            this._previousState = new User.Settings()
            {
                GetEpisodeTitles = User.Settings.Get().GetEpisodeTitles,
                IncludeSubtitles = User.Settings.Get().IncludeSubtitles,
                DeleteNonMediaFiles = User.Settings.Get().DeleteNonMediaFiles,
                SearchSubDirectories = User.Settings.Get().SearchSubDirectories,
                VideoExtensions = User.Settings.Get().VideoExtensions,
                SubtitleExtensions = User.Settings.Get().SubtitleExtensions,
            };

            // set commands
            this._applyButtonCommand = new Commands.RelayCommand(this.applyButtonClick, true);
            this._cancelButtonCommand = new Commands.RelayCommand(this.cancelButtonClick, true);
        }
        #endregion

        #region methods
        /// <summary>
        /// Update global settings state to reflect changes
        /// </summary>
        private void applyButtonClick()
        {
            // update global user settings
            User.Settings.Get().GetEpisodeTitles = this.GetEpisodeTitles;
            User.Settings.Get().IncludeSubtitles = this.IncludeSubtitles;
            User.Settings.Get().DeleteNonMediaFiles = this.DeleteNonMediaFiles;
            User.Settings.Get().SearchSubDirectories = this.SearchSubDirectories;

            // this new state is now "previous state"
            this.updatePreviousState();

            // save settings
            this.saveSettings();
        }

        /// <summary>
        /// Revert global settings state to previous state
        /// </summary>
        private void cancelButtonClick()
        {
            this.GetEpisodeTitles = this._previousState.GetEpisodeTitles;
            this.IncludeSubtitles = this._previousState.IncludeSubtitles;
            this.DeleteNonMediaFiles = this._previousState.DeleteNonMediaFiles;
            this.SearchSubDirectories = this._previousState.SearchSubDirectories;
        }

        /// <summary>
        /// Updates previous state based on values in settings window
        /// </summary>
        private void updatePreviousState()
        {
            this._previousState.GetEpisodeTitles = this.GetEpisodeTitles;
            this._previousState.IncludeSubtitles = this.IncludeSubtitles;
            this._previousState.DeleteNonMediaFiles = this.DeleteNonMediaFiles;
            this._previousState.SearchSubDirectories = this.SearchSubDirectories;
        }

        /// <summary>
        /// Save the settings to the users settings file
        /// </summary>
        private void saveSettings()
        {            
            Properties.Settings.Default.GetEpisodeTitles = User.Settings.Get().GetEpisodeTitles;
            Properties.Settings.Default.IncludeSubtitles = User.Settings.Get().IncludeSubtitles;
            Properties.Settings.Default.DeleteNonMediaFiles = User.Settings.Get().DeleteNonMediaFiles;
            Properties.Settings.Default.Save();
            //Properties.Settings.Default.SearchSubDirectories = this.SearchSubDirectories;
        }
        #endregion
    }
}