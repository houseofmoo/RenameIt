using System;
using System.Collections.Generic;

namespace RenameIt.ViewModels.Directory
{
    /// <summary>
    /// ViewModel for a video directory item
    /// </summary>
    public class VideoViewModel : ItemViewModel
    {
        public VideoViewModel(Models.DirectoryItem item) : base(item) { }
    }
}
