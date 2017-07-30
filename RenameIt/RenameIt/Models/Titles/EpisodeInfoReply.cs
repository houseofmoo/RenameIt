using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Models.Titles
{
    /// <summary>
    /// The json reply contains a lot of information we do not care 
    /// about. Only store the information we do care about into this class
    /// </summary>
    public class EpisodeInfoReply
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
