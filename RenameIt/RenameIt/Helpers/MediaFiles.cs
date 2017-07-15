using System;
using System.Collections.Generic;
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
                if (System.Windows.Forms.DialogResult.OK == dialog.ShowDialog())
                {
                    files = MediaFiles.getDirectoryItemListFromPath(dialog.SelectedPath);
                }

                return files;
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
                }));
            }
            catch (Exception e)
            {
                // TODO - Deal with exception
            }

            return items;
        }
    }
}
