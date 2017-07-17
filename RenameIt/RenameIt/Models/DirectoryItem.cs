using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Models
{
    /// <summary>
    /// Represents a file inside a directory.
    /// </summary>
    class DirectoryItem
    {
        #region private fields
        private string _fullPath;
        private string _directory;
        private string _name;
        #endregion

        #region public properties
        public string FullPath
        {
            get { return _fullPath; }
            set { _fullPath = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        } 
        #endregion
    }
}
