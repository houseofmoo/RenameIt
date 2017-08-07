using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.ViewModels.Directory
{
    public class ItemViewModel : Base.ViewModel
    {
        #region private fields
        private string _fullPath;
        private string _directory;
        private string _name;
        private string _extension;
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
                if (_fullPath == value)
                    return;
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
                if (_directory == value)
                    return;
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
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Extension of the file
        /// </summary>
        public string Extension
        {
            get { return _extension; }
            set
            {
                if (_extension == value)
                    return;
                _extension = value;
                OnPropertyChanged(nameof(Extension));
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
                if (_newName == value)
                    return;
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
            this.Extension = item.Extension;
            this.NewName = string.Empty;
        }
        #endregion

        #region methods
        public void SetNewName(string showname, string season, int epNum, string title)
        {
            // build new name
            this.NewName = $"{showname} - " +
                $"S{addLeadingZeroes(season)}" +
                $"E{addLeadingZeroes(epNum.ToString())}";

            if (title != string.Empty)
                this.NewName += $" - {title}";

            this.NewName += this.Extension;
        }

        /// <summary>
        /// Adds leading zeros to a string that represents a number.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string addLeadingZeroes(string num)
        {
            try
            {
                // convert to a integer
                int _num = Convert.ToInt32(num);

                // add leading 0 if needed, return number
                return _num < 10 ? "0" + _num.ToString() : _num.ToString();
            }
            catch
            {
                // if we had an error, arriving string wasn't a number
                // return it and continue
                return num;
            }
        }
        #endregion
    }
}
