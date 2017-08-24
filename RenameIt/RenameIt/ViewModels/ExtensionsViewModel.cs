using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RenameIt.ViewModels
{
    public class ExtensionsViewModel : Common.ViewModels.BaseViewModel
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
        public string VideoExtensionTitle { get => VIDEO_EXTENSIONS; }
        public string SubtitleExtensionsTitle { get => SUBTITLE_EXTENSIONS; }

        public ICommand AddVideoExtensionCommand { get => _addVideoExtensionCommand; }
        public ICommand AddSubtitleExtensionCommand { get => _addSubtitleExtensionCommand; }

        public ICommand DeleteVideoExtensionCommand { get => _deleteVideoExtensionCommand; }
        public ICommand DeleteSubtitleExtensionCommand { get => _deleteSubtitleExtensionCommand; }

        /// <summary>
        /// The currently selected item in the video extensions list box
        /// </summary>
        public string SelectedVideoExtension
        {
            get => _selectedVideoExtension;
            set => SetProperty(ref _selectedVideoExtension, value);
        }

        /// <summary>
        /// The currently selected item in the video extensions list box
        /// </summary>
        public string SelectedSubtitleExtension
        {
            get => _selectedSubtitleExtension;
            set => SetProperty(ref _selectedSubtitleExtension, value);
        }

        /// <summary>
        /// Text within the video extension text box
        /// </summary>
        public string VideoExtensionTextBoxText
        {
            get => _videoExtensionTextBoxText;
            set => SetProperty(ref _videoExtensionTextBoxText, value);
        }

        /// <summary>
        /// Text within the subtitle exntension text box
        /// </summary>
        public string SubtitleExtensionTextBoxText
        {
            get => _subtitleExtensionTextBoxText;
            set => SetProperty(ref _subtitleExtensionTextBoxText, value);
        }

        /// <summary>
        /// List containing video exntesions for display
        /// </summary>
        public ObservableCollection<string> VideoExtensionsList
        {
            get => _videoExtensionsList;
            set => SetProperty(ref _videoExtensionsList, value);
        }

        /// <summary>
        /// List containing subtitle extensions for display
        /// </summary>
        public ObservableCollection<string> SubtitleExtensionsList
        {
            get => _subtitleExtensionsList;
            set => SetProperty(ref _subtitleExtensionsList, value);
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
            this._addVideoExtensionCommand = new Common.Commands.RelayCommand<object>(addVideoExtensionExecute);
            this._addSubtitleExtensionCommand = new Common.Commands.RelayCommand<object>(addSubtitleExtensionExecute);
            this._deleteVideoExtensionCommand = new Common.Commands.RelayCommand<object>(deleteVideoExtension, deleteVideoExtensionCanExecute);
            this._deleteSubtitleExtensionCommand = new Common.Commands.RelayCommand<object>(deleteSubtitleExtension, deleteSubtitleExtensionCanExecute);
        }
        #endregion

        #region methods
        /// <summary>
        /// Adds video extension from text box
        /// </summary>
        private void addVideoExtensionExecute(object obj)
        {
            // error check
            if (string.IsNullOrWhiteSpace(this.VideoExtensionTextBoxText))
                return;
            else if (this.VideoExtensionTextBoxText[0] != '.')
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
        private void addSubtitleExtensionExecute(object obj)
        {
            // erorr check
            if (string.IsNullOrWhiteSpace(this.SubtitleExtensionTextBoxText))
                return;
            else if (this.SubtitleExtensionTextBoxText[0] != '.')
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
        private void deleteVideoExtension(object obj)
        {
            // error check
            if (this.SelectedVideoExtension == null)
                return;

            // remove from settings list
            Properties.Settings.Default.VideoExtensions.Remove(this.SelectedVideoExtension);

            // remove from list
            this.VideoExtensionsList.Remove(this.SelectedVideoExtension);
        }

        private bool deleteVideoExtensionCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(this.SelectedVideoExtension) &&
                this.VideoExtensionsList != null &&
                this.VideoExtensionsList.Any() &&
                this.VideoExtensionsList.Contains(this.SelectedVideoExtension);
                
        }

        /// <summary>
        /// Deleted selected subtitle extensions
        /// </summary>
        private void deleteSubtitleExtension(object obj)
        {
            // error echeck
            if (this._selectedSubtitleExtension == null)
                return;

            // remove from settings list
            Properties.Settings.Default.SubtitleExtensions.Remove(this.SelectedSubtitleExtension);

            // remove from list
            this.SubtitleExtensionsList.Remove(this.SelectedSubtitleExtension);
        }

        private bool deleteSubtitleExtensionCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(this.SelectedSubtitleExtension) &&
                this.SubtitleExtensionsList != null &&
                this.SubtitleExtensionsList.Any() &&
                this.SubtitleExtensionsList.Contains(this.SelectedSubtitleExtension);
        }
        #endregion
    }
}
