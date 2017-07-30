using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Models.Titles
{
    /// <summary>
    /// THe reply from the show ID request contains a lot of data we do not 
    /// care about. We only care about the ID value, which this class contains
    /// </summary>
    public class ShowIdReply
    {
        public int id { get; set; }
    }
}
