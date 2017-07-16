using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Models.Titles
{
    /// <summary>
    /// The json object structure of a Episode Info request to API.
    /// </summary>
    public class EpisodeInfoReply
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public int season { get; set; }
        public int number { get; set; }
        public string airdate { get; set; }
        public string airtime { get; set; }
        public DateTime airstamp { get; set; }
        public int runtime { get; set; }
        public epImage image { get; set; }
        public string summary { get; set; }
        public ep_Links _links { get; set; }
    }

    public class epImage
    {
        public string medium { get; set; }
        public string original { get; set; }
    }

    public class ep_Links
    {
        public epSelf self { get; set; }
    }

    public class epSelf
    {
        public string href { get; set; }
    }
}
