using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.ViewModels
{
    class ExtensionsViewModel : Base.ViewModel
    {
        #region private constants
        private const string VIDEO_EXTENSIONS = "Video Extensions";
        private const string SUBTITLE_EXTENSIONS = "Subtitle Extensions";
        #endregion

        #region private fields
        private string _videoExtensionsText;
        private string _subtitleExtensionsText;
        #endregion

        #region public properties
        public string VideoExtensionTitle { get { return VIDEO_EXTENSIONS; } }
        public string SubtitleExtensionsTitle { get { return SUBTITLE_EXTENSIONS; } }

        public string VideoExtensionsText
        {
            get { return _videoExtensionsText; }
            set
            {
                if (_videoExtensionsText == value)
                    return;
                _videoExtensionsText = value;
                // update video extensions list
                OnPropertyChanged(nameof(VideoExtensionsText));
            }
        }

        public string SubtitleExtensionsText
        {
            get { return _subtitleExtensionsText; }
            set
            {
                if (_subtitleExtensionsText == value)
                    return;
                _subtitleExtensionsText = value;
                // update subtitle extensions list
                OnPropertyChanged(nameof(SubtitleExtensionsText));
            }
        }
        #endregion

        #region constructor
        public ExtensionsViewModel()
        {
            // video
            string videoText = "";
            foreach (var ext in Identifiers.Extensions.VideoDefaults)
                videoText += ext + ", ";
            this.VideoExtensionsText = videoText.Substring(0, videoText.Length - 2);

            // extension
            string subText = "";
            foreach (var ext in Identifiers.Extensions.SubtitleDefaults)
                subText += ext + ", ";
            this.SubtitleExtensionsText = subText.Substring(0, subText.Length - 2);
        }
        #endregion
    }
}
