using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.User
{
    /// <summary>
    /// Represents user settings. This object contains a private 
    /// static instance of itself. This is the global settings state
    /// and can be retrieved via <see cref="Settings.Get()"/>
    /// </summary>
    public class Settings
    {
        #region private static fields
        private static Settings _userSettings;
        #endregion

        #region private fields
        //private bool _getEpisodeTitles;
        //private bool _includeSubtitles;
        //private bool _deleteNonMediaFiles;
        //private bool _searchSubDirectories;
        //private List<string> _videoExtensions;
        //private List<string> _subtitleExtensions;
        #endregion

        /// <summary>
        /// Does the user wish to fetch episode titles
        /// </summary>
        public bool GetEpisodeTitles
        {
            get;
            set;
        }

        /// <summary>
        /// Does the user wish to include subtitle file types
        /// </summary>
        public bool IncludeSubtitles
        {
            get;
            set;
        }

        /// <summary>
        /// Does the user wish to delete non-media file types
        /// </summary>
        public bool DeleteNonMediaFiles
        {
            get;
            set;
        }

        /// <summary>
        /// Does the user wish to search sub directories for valid file types
        /// </summary>
        public bool SearchSubDirectories
        {
            get;
            set;
        }

        /// <summary>
        /// List of valid video extensions
        /// </summary>
        public List<string> VideoExtensions
        {
            get;
            set;
        }

        /// <summary>
        /// List of valid subtitle extensions
        /// </summary>
        public List<string> SubtitleExtensions
        {
            get;
            set;
        }

        #region methods
        /// <summary>
        /// Returns the global settings state
        /// </summary>
        /// <returns></returns>
        public static Settings Get()
        {
            // on first retrieval we create the global settings list
            if (_userSettings == null)
            {
                _userSettings = new Settings();

                _userSettings.GetEpisodeTitles = Properties.Settings.Default.GetEpisodeTitles;
                _userSettings.IncludeSubtitles = Properties.Settings.Default.IncludeSubtitles;
                _userSettings.DeleteNonMediaFiles = Properties.Settings.Default.DeleteNonMediaFiles;
                _userSettings.SearchSubDirectories = false;

                // set up video extensions list
                _userSettings.VideoExtensions = new List<string>();
                foreach (var ext in Properties.Settings.Default.VideoExtensions)
                    _userSettings.VideoExtensions.Add(ext);


                // set up subtitle extensions list
                _userSettings.SubtitleExtensions = new List<string>();
                foreach (var ext in Properties.Settings.Default.SubtitleExtensions)
                    _userSettings.SubtitleExtensions.Add(ext);
            }
                
            return _userSettings;
        } 
        #endregion
    }
}
