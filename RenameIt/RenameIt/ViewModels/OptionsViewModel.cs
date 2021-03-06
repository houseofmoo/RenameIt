﻿using System.Windows.Input;

namespace RenameIt.ViewModels
{
    /// <summary>
    /// Allows user to define their personal settings for RenameIt
    /// </summary>
    class OptionsViewModel : Base.ViewModel
    {
        #region private constants
        // check box content values
        private const string GET_EPISODE_TITLES = "Get Episode Titles";
        private const string INCLUDE_SUBTITLES = "Include Subtitles";
        private const string DELETE_NONMEDIA_FILES = "Delete Non-Media Files";
        private const string SEARCH_SUBDIRECTORIES = "Search Sub-directories";
        #endregion

        #region private fields
        // check box values
        private bool _getEpisodeTitles = Properties.Settings.Default.GetEpisodeTitles;
        private bool _includeSubtitles = Properties.Settings.Default.IncludeSubtitles;
        private bool _deleteNonMediaFiles = Properties.Settings.Default.DeleteNonMediaFiles;
        private bool _searchSubdirectories = false;
        #endregion

        #region checkbox properties
        /// <summary>
        /// Stores the checkbox value of Get Episode Titles, updates global settings
        /// </summary>
        public bool GetEpisodeTitles
        {
            get { return _getEpisodeTitles; }
            set
            {
                if (_getEpisodeTitles == value)
                    return;
                _getEpisodeTitles = value;
                Properties.Settings.Default.GetEpisodeTitles = value;
                OnPropertyChanged(nameof(GetEpisodeTitles));
            }
        }

        /// <summary>
        /// Stores the checkbox value of Ignore Subtitles, updates global settings
        /// </summary>
        public bool IncludeSubtitles
        {
            get { return _includeSubtitles; }
            set
            {
                if (_includeSubtitles == value)
                    return;
                _includeSubtitles = value;
                Properties.Settings.Default.IncludeSubtitles = value;
                OnPropertyChanged(nameof(IncludeSubtitles));
            }
        }

        /// <summary>
        /// Stores the checkbox value of Delete Non-Media Files, updates global settings
        /// </summary>
        public bool DeleteNonMediaFiles
        {
            get { return _deleteNonMediaFiles; }
            set
            {
                if (_deleteNonMediaFiles == value)
                    return;
                _deleteNonMediaFiles = value;
                Properties.Settings.Default.DeleteNonMediaFiles = value;
                OnPropertyChanged(nameof(DeleteNonMediaFiles));
            }
        }

        /// <summary>
        /// Stores the checkbox value of Search Subdirectories, updates global settings
        /// </summary>
        public bool SearchSubDirectories
        {
            get { return _searchSubdirectories; }
            set
            {
                if (_searchSubdirectories == value)
                    return;
                _searchSubdirectories = value;
                //Properties.Settings.Default.SearchSubDirectories = value;
                OnPropertyChanged(nameof(SearchSubDirectories));
            }
        }

        public string GetEpisodeTitlesContent { get { return GET_EPISODE_TITLES; } }
        public string IncludeSubtitlesContent { get { return INCLUDE_SUBTITLES; } }
        public string DeleteNonMediaFilesContent { get { return DELETE_NONMEDIA_FILES; } }
        public string SearchSubDirectoriesContent { get { return SEARCH_SUBDIRECTORIES; } }
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public OptionsViewModel()
        {
            // nothing to do!
        }
        #endregion
    }
}
