using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RenameIt.Helpers
{
    static class MediaFiles
    {
        /// <summary>
        /// Returns a list of <see cref="Models.DirectoryItem"/> from provided path.
        /// </summary>
        /// <returns></returns>
        public static List<Models.DirectoryItem> Get(string path)
        {
            var files = Directory.GetFiles(path);

            var items = new List<Models.DirectoryItem>();

            items.AddRange(files.Select(file => new Models.DirectoryItem
            {
                FullPath = file,
                Name = getNameFromPath(file),
                Directory = Path.GetDirectoryName(file)
            }));

            return items;
        }

        /// <summary> 
        /// Attempts to retreive files from selected directory.
        /// </summary>
        /// <param name="path"></param>
        public static List<Models.DirectoryItem> GetFilesFromDirectory()
        {
            var files = new List<Models.DirectoryItem>();
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (System.Windows.Forms.DialogResult.OK == dialog.ShowDialog())
                {
                    files = Helpers.MediaFiles.Get(dialog.SelectedPath);
                }

                return files;
            }
        }

        /// <summary>
        /// Gets name of file from full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string getNameFromPath(string path)
        {
            // split by separators
            string[] temp = path.Split(new string[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries);

            if (temp == null)
                // error, return empty string
                return string.Empty;
            else if (temp.Length == 0)
                // if we didnt have any separators, then path is name of file
                return path;

            // return last item which is the name
            return temp[temp.Length - 1];
        }
    }
}
