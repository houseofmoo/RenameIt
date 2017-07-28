using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace RenameIt.Helpers
{
    static class MediaFiles
    {
        /// <summary> 
        /// Attempts to retreive files from selected directory.
        /// </summary>
        /// <param name="path"></param>
        public static List<Models.DirectoryItem> GetFilesFromDirectory()
        {
            var files = new List<Models.DirectoryItem>();
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                // personal use
                dialog.SelectedPath = @"E:\FileZilla\";

                if (System.Windows.Forms.DialogResult.OK == dialog.ShowDialog())
                    files = MediaFiles.getDirectoryItemListFromPath(dialog.SelectedPath);

                return files;
            }
        }

        /// <summary>
        /// Grabs only items from the files list that match video file extensions
        /// </summary>
        /// <param name="files">List of files pulled from directory</param>
        /// <returns></returns>
        public static List<Models.DirectoryItem> GetOnlyVideoFiles(List<Models.DirectoryItem> files)
        {
            var videoFiles = new List<Models.DirectoryItem>();
            foreach (var file in files)
            {
                // only add files that match 
                if (Identifiers.Extensions.Video.Contains(file.Extension.ToLower()))
                    videoFiles.Add(file);
            }

            return videoFiles;
        }

        /// <summary>
        /// Grabs items from the files list that match video file or subtitle extensions
        /// </summary>
        /// <param name="files">List of files pulled from directory</param>
        /// <returns></returns>
        public static List<Models.DirectoryItem> GetVideoAndSubtitleFiles(List<Models.DirectoryItem> files)
        {
            var videoFiles = new List<Models.DirectoryItem>();
            foreach (var file in files)
            {
                // only add files that match 
                if (Identifiers.Extensions.Video.Contains(file.Extension.ToLower()) ||
                    Identifiers.Extensions.Subtitle.Contains(file.Extension.ToLower()))
                    videoFiles.Add(file);
            }

            return videoFiles;
        }

        /// <summary>
        /// Changes file names to newly defined names.
        /// </summary>
        public static void UpdateFileNames(ObservableCollection<ViewModels.Directory.VideoItemViewModel> items) {
            foreach (var item in items) {
                // remove illegal characters
                item.NewName = removeIllegalFileCharacters(item.NewName);

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
        private static List<Models.DirectoryItem> getDirectoryItemListFromPath(string path)
        {
            // value to be returned
            var items = new List<Models.DirectoryItem>();

            // try get the files from the directory and add them to items list
            try
            {
                var dirInfo = new DirectoryInfo(path);
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
        private static string removeIllegalFileCharacters(string title) {
            // get a list off all invalid characters
            string illegal = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            // remove and item that matches a illegal character
            foreach (var c in illegal)
                title = title.Replace(c.ToString(), "");

            return title;
        }
    }
}
