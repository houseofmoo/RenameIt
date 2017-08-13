using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    class ExtensionsViewModel : Base.ViewModel
    {
        #region private constants
        private const string VIDEO_EXTENSIONS = "Video Extensions";
        private const string SUBTITLE_EXTENSIONS = "Subtitle Extensions";
        #endregion

        #region private fields
        private ICommand _addVideoExtensionCommand;
        private ICommand _addSubtitleExtensionCommand;

        private ICommand _deleteVideoExtensionCommand;
        private ICommand _deleteSubtitleExtensionCommand;

        private string _selectedVideoExtension;
        private string _selectedSubtitleExtension;

        private string _videoExtensionTextBoxText;
        private string _subtitleExtensionTextBoxText;

        private ObservableCollection<string> _videoExtensionsList;
        private ObservableCollection<string> _subtitleExtensionsList;
        #endregion

        #region public properties
        public string VideoExtensionTitle { get { return VIDEO_EXTENSIONS; } }
        public string SubtitleExtensionsTitle { get { return SUBTITLE_EXTENSIONS; } }

        public ICommand AddVideoExtensionCommand { get { return _addVideoExtensionCommand; } }
        public ICommand AddSubtitleExtensionCommand { get { return _addSubtitleExtensionCommand; } }

        public ICommand DeleteVideoExtensionCommand { get { return _deleteVideoExtensionCommand; } }
        public ICommand DeleteSubtitleExtensionCommand { get { return _deleteSubtitleExtensionCommand; } }

        /// <summary>
        /// The currently selected item in the video extensions list box
        /// </summary>
        public string SelectedVideoExtension
        {
            get { return _selectedVideoExtension; }
            set
            {
                if (_selectedVideoExtension == value)
                    return;
                _selectedVideoExtension = value;
                OnPropertyChanged(nameof(SelectedVideoExtension));
            }
        }

        /// <summary>
        /// The currently selected item in the video extensions list box
        /// </summary>
        public string SelectedSubtitleExtension
        {
            get { return _selectedSubtitleExtension; }
            set
            {
                if (_selectedSubtitleExtension == value)
                    return;
                _selectedSubtitleExtension = value;
                OnPropertyChanged(nameof(SelectedSubtitleExtension));
            }
        }

        /// <summary>
        /// Text within the video extension text box
        /// </summary>
        public string VideoExtensionTextBoxText
        {
            get { return _videoExtensionTextBoxText; }
            set
            {
                if (_videoExtensionTextBoxText == value)
                    return;
                _videoExtensionTextBoxText = value;
                OnPropertyChanged(nameof(VideoExtensionTextBoxText));
            }
        }

        /// <summary>
        /// Text within the subtitle exntension text box
        /// </summary>
        public string SubtitleExtensionTextBoxText
        {
            get { return _subtitleExtensionTextBoxText; }
            set
            {
                if (_subtitleExtensionTextBoxText == value)
                    return;
                _subtitleExtensionTextBoxText = value;
                OnPropertyChanged(nameof(SubtitleExtensionTextBoxText));
            }
        }

        /// <summary>
        /// List containing video exntesions for display
        /// </summary>
        public ObservableCollection<string> VideoExtensionsList
        {
            get { return _videoExtensionsList; }
            set
            {
                if (_videoExtensionsList == value)
                    return;
                _videoExtensionsList = value;
                OnPropertyChanged(nameof(VideoExtensionsList));
            }
        }

        /// <summary>
        /// List containing subtitle extensions for display
        /// </summary>
        public ObservableCollection<string> SubtitleExtensionsList
        {
            get { return _subtitleExtensionsList; }
            set
            {
                if (_subtitleExtensionsList == value)
                    return;
                _subtitleExtensionsList = value;
                OnPropertyChanged(nameof(SubtitleExtensionsList));
            }
        }
        #endregion

        #region constructor
        public ExtensionsViewModel()
        {
            // video
            this.VideoExtensionsList = new ObservableCollection<string>(Properties.Settings.Default.VideoExtensions.Cast<string>().ToList());

            // extension
            this.SubtitleExtensionsList = new ObservableCollection<string>(Properties.Settings.Default.SubtitleExtensions.Cast<string>().ToList());

            // commands
            this._addVideoExtensionCommand = new Commands.RelayCommand(addVideoExtension, true);
            this._addSubtitleExtensionCommand = new Commands.RelayCommand(addSubtitleExtension, true);
            this._deleteVideoExtensionCommand = new Commands.RelayCommand(deleteVideoExtension, true);
            this._deleteSubtitleExtensionCommand = new Commands.RelayCommand(deleteSubtitleExtension, true);
        }
        #endregion

        #region methods
        /// <summary>
        /// Adds video extension from text box
        /// </summary>
        private void addVideoExtension()
        {
            // error check
            if (string.IsNullOrWhiteSpace(this.VideoExtensionTextBoxText))
                return;

            if (!this.VideoExtensionsList.Contains(this.VideoExtensionTextBoxText))
            {
                // add to video extensions list
                this.VideoExtensionsList.Add(this.VideoExtensionTextBoxText);

                // update video extensions list settings item
                Properties.Settings.Default.VideoExtensions.Add(this.VideoExtensionTextBoxText);
            }

            // empty text box
            this.VideoExtensionTextBoxText = string.Empty;
        }

        /// <summary>
        /// Add subtitle extension from text box
        /// </summary>
        private void addSubtitleExtension()
        {
            // erorr check
            if (string.IsNullOrWhiteSpace(this.SubtitleExtensionTextBoxText))
                return;

            if (!this.SubtitleExtensionsList.Contains(this.SubtitleExtensionTextBoxText))
            {
                // add to subtitle extensions list
                this.SubtitleExtensionsList.Add(this.SubtitleExtensionTextBoxText);

                // update settings subtitle extensions list
                Properties.Settings.Default.SubtitleExtensions.Add(this.SubtitleExtensionTextBoxText);
            }

            // empty text box
            this.SubtitleExtensionTextBoxText = string.Empty;
        }

        /// <summary>
        /// Deletes selected video extension
        /// </summary>
        private void deleteVideoExtension()
        {
            // error check
            if (this.SelectedVideoExtension == null)
                return;

            // remove from settings list
            Properties.Settings.Default.VideoExtensions.Remove(this.SelectedVideoExtension);

            // remove from list
            this.VideoExtensionsList.Remove(this.SelectedVideoExtension);
        }

        /// <summary>
        /// Deleted selected subtitle extensions
        /// </summary>
        private void deleteSubtitleExtension()
        {
            // error echeck
            if (this._selectedSubtitleExtension == null)
                return;

            // remove from settings list
            Properties.Settings.Default.SubtitleExtensions.Remove(this.SelectedSubtitleExtension);

            // remove from list
            this.SubtitleExtensionsList.Remove(this.SelectedSubtitleExtension);
        }
        #endregion
    }
}
