using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.ViewModels.Directory
{
    /// <summary>
    /// ViewModel for a subtitle directory item
    /// </summary>
    public class SubtitleViewModel : ItemViewModel
    {
        public SubtitleViewModel(Models.DirectoryItem item) : base(item) { }
    }
}
