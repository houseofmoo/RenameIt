using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace RenameIt.Helpers
{
    /// <summary>
    /// Helper class that can deal with retreiving and manipulating files
    /// </summary>
    static class MediaFiles
    {
        /// <summary> 
        /// Attempts to retreive files from selected directory.
        /// </summary>
        /// <param name="path"></param>
        public static List<Models.DirectoryItem> GetFilesFromDirectory(out string directory)
        {
            var files = new List<Models.DirectoryItem>();
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                // personal use
                dialog.SelectedPath = @"E:\FileZilla\";

                // show file dialog box and get directory items from selected directory
                if (System.Windows.Forms.DialogResult.OK == dialog.ShowDialog())
                {
                    files = MediaFiles.getDirectoryItemListFromPath(dialog.SelectedPath);
                    directory = dialog.SelectedPath;
                    return files;
                }
                else
                {
                    directory = string.Empty;
                    return files;
                }

            }
        }

        /// <summary>
        /// Grabs items from the files list that match valid formats.
        /// </summary>
        /// <param name="files">Files from the directory</param>
        /// <param name="validFormat">Valid formats we care about</param>
        /// <returns></returns>
        public static List<Models.DirectoryItem> GetMatchingFiles(List<Models.DirectoryItem> files, List<string> validExtensions)
        {
            // list of valid files
            var validFiles = new List<Models.DirectoryItem>();

            // only add files that match valid extension 
            foreach (var file in files)
            {
                if (validExtensions.Contains(file.Extension.ToLower()))
                    validFiles.Add(file);
            }

            return validFiles;
        }

        /// <summary>
        /// Deletes file that do not match Video or Subtitle extensions
        /// </summary>
        /// <param name="files"></param>
        /// <param name="validFormat"></param>
        public static void DeleteInvalidFiles(string directory)
        {
            // ensure directory exists
            if (!Directory.Exists(directory))
                return;

            // length of the new list
            int length = Properties.Settings.Default.VideoExtensions.Count + Properties.Settings.Default.SubtitleExtensions.Count;

            // create new list
            var validExtensions = new string[length];

            // place all valid extensions into new list
            Properties.Settings.Default.VideoExtensions.CopyTo(validExtensions, 0);
            Properties.Settings.Default.SubtitleExtensions.CopyTo(validExtensions, Properties.Settings.Default.VideoExtensions.Count);

            // get all files from directory
            var files = getDirectoryItemListFromPath(directory);

            // delete files where their extension is not valid
            foreach (var file in files)
            {
                if (!validExtensions.Contains(file.Extension.ToLower()))
                    File.Delete(file.FullPath);
            }
        }

        /// <summary>
        /// Changes file names to newly defined names.
        /// </summary>
        public static void UpdateFileNames(ObservableCollection<ViewModels.Directory.ItemViewModel> items)
        {
            foreach (var item in items)
            {
                // remove illegal characters
                item.NewName = removeIllegalFileCharacters(item.NewName);

                // check if theres a new name to update to and that the file does not already exist
                if (item.NewName == string.Empty || File.Exists(item.Directory + "\\" + item.NewName))
                    return;

                // change name
                File.Move(item.FullPath, item.Directory + "\\" + item.NewName);

                // update path
                item.FullPath = item.Directory + "\\" + item.NewName;

                // new name is now the name
                item.Name = item.NewName;

                // new name is now empty
                item.NewName = string.Empty;
            }
        }

        /// <summary>
        /// Returns a list of <see cref="Models.DirectoryItem"/> from provided path.
        /// </summary>
        /// <returns></returns>
        private static List<Models.DirectoryItem> getDirectoryItemListFromPath(string directoryPath)
        {
            // value to be returned
            var items = new List<Models.DirectoryItem>();

            // try get the files from the directory and add them to items list
            try
            {
                var dirInfo = new DirectoryInfo(directoryPath);
                var fileInfo = dirInfo.GetFiles();

                // add all files to items list
                items.AddRange(fileInfo.Select(file => new Models.DirectoryItem
                {
                    FullPath = file.FullName,
                    Name = file.Name,
                    Directory = file.DirectoryName,
                    Extension = file.Extension,
                }));
            }
            catch (Exception e)
            {
                e.ToString();
                // TODO - Deal with exception
            }

            return items;
        }

        /// <summary>
        /// Removes illegal environment characters from file paths
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private static string removeIllegalFileCharacters(string title)
        {
            // get a list off all invalid characters
            string illegal = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            // remove and item that matches a illegal character
            foreach (var c in illegal)
                title = title.Replace(c.ToString(), "");

            return title;
        }
    }
}
