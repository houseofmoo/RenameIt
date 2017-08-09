using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Identifiers
{
    /// <summary>
    /// Contains lists of default valid file extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Default video file valid extensions
        /// </summary>
        private static readonly string[] _videoDefault = { ".avi", ".mkv", ".mp4", ".wmv", ".mov" };

        /// <summary>
        /// Default subtitle file valid extensions
        /// </summary>
        private static readonly string[] _subtitleDefault = { ".srt" };

        public static string[] VideoDefaults {  get { return _videoDefault; } }
        public static string[] SubtitleDefaults { get { return _subtitleDefault; } }

        /// <summary>
        /// Reset video and subtitle extensions to defaults
        /// </summary>
        public static void ResetDefaultExtensions()
        {
            throw new NotImplementedException();
        }
    }
}
