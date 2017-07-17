namespace RenameIt.ViewModels.Directory
{
    /// <summary>
    /// ViewModel for a directory item.
    /// </summary>
    class ItemViewModel : Base.ViewModel
    {
        #region private fields
        private string _fullPath;
        private string _directory;
        private string _name;
        private string _newName;
        #endregion

        #region public properties
        /// <summary>
        /// Full path to file.
        /// </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                if (_fullPath == value) return;
                _fullPath = value;
                OnPropertyChanged(nameof(FullPath));
            }
        }

        /// <summary>
        /// Returns the directory this item is located in.
        /// </summary>
        public string Directory
        {
            get { return _directory; }
            set
            {
                if (_directory == value) return;
                _directory = value;
                OnPropertyChanged(nameof(Directory));
            }
        }

        /// <summary>
        /// Name of file.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// The new name of the item
        /// </summary>
        public string NewName
        {
            get { return _newName; }
            set
            {
                if (_newName == value) return;
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }

        /// <summary>
        /// Returns an arrow symbol.
        /// </summary>
        public string Arrow { get { return "=>"; } }
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ItemViewModel()
        {
            FullPath = string.Empty;
            Directory = string.Empty;
            Name = string.Empty;
            NewName = string.Empty;
        }

        /// <summary>
        /// Construct a new <see cref="DirectoryItemViewModel"/> using a <see cref="Models.DirectoryItem"/> object.
        /// </summary>
        /// <param name="item"></param>
        public ItemViewModel(Models.DirectoryItem item)
        {
            this.FullPath = item.FullPath;
            this.Name = item.Name;
            this.Directory = item.Directory;
            NewName = string.Empty;
        }
        #endregion
    }
}
