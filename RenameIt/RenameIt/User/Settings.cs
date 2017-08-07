using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.User
{
    /// <summary>
    /// Represents user settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Does the user wish to fetch episode titles
        /// </summary>
        public static bool GetEpisodeTitles { get; set; } = Properties.Settings.Default.GetTitles;

        /// <summary>
        /// Does the user wish to include subtitle file types
        /// </summary>
        public static bool IncludeSubtitles { get; set; } = Properties.Settings.Default.IncludeSubtitles;

        /// <summary>
        /// Does the user wish to delete non-media file types
        /// </summary>
        public static bool DeleteNonMediaFiles { get; set; } = Properties.Settings.Default.DeleteNonMediaFiles;

        /// <summary>
        /// Does the user wish to search sub directories for valid file types
        /// </summary>
        public static bool SearchSubdirectories { get; set; } = false;

        /// <summary>
        /// List of valid video extensions
        /// </summary>
        public static List<string> VideoExtensions = new List<string>();

        /// <summary>
        /// List of valid subtitle extensions
        /// </summary>
        public static List<string> SubtitleExtensions = new List<string>();
    }
}
