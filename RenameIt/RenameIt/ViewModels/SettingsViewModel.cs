using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.ViewModels
{
    /// <summary>
    /// ViewModel for the settings window
    /// </summary>
    class SettingsViewModel : Base.ViewModel
    {
        #region private constants
        // check box content values
        private const string GET_EPISODE_TITLES = "Get Episode Titles";
        private const string INCLUDE_SUBTITLES = "Include Subtitles";
        private const string DELETE_NONMEDIA_FILES = "Delete Non-media Files";
        private const string SEARCH_SUBDIRECTORIES = "Search Sub-directories";
        #endregion

        #region checkbox properties
        /// <summary>
        /// Stores the checkbox value of Get Episode Titles
        /// </summary>
        public bool GetEpisodeTitles
        {
            get { return User.Settings.GetEpisodeTitles; }
            set
            {
                if (User.Settings.GetEpisodeTitles == value)
                    return;
                User.Settings.GetEpisodeTitles = value;
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
            get { return User.Settings.IncludeSubtitles; }
            set
            {
                if (User.Settings.IncludeSubtitles == value)
                    return;
                User.Settings.IncludeSubtitles = value;
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
            get { return User.Settings.DeleteNonMediaFiles; }
            set
            {
                if (User.Settings.DeleteNonMediaFiles == value)
                    return;
                User.Settings.DeleteNonMediaFiles = value;
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
            get { return User.Settings.SearchSubdirectories; }
            set
            {
                if (User.Settings.SearchSubdirectories == value)
                    return;
                User.Settings.SearchSubdirectories = value;
                OnPropertyChanged(nameof(SearchSubDirectories));
            }
        }

        /// <summary>
        /// Returns the content for Search Subdirectories checkbox
        /// </summary>
        public string SearchSubDirectoriesContent { get { return SEARCH_SUBDIRECTORIES; } }
        #endregion

        #region constructors
        public SettingsViewModel()
        {
            // todo
        }
        #endregion
    }
}
