﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Helpers.Titles
{
    public class Request
    {
        public string ShowName { get; set; }
        public string Season { get; set; }
        public string EpisodeBegin { get; set; }
        public int EpisodeCount { get; set; }

        public Request()
        {
            this.ShowName = string.Empty;
            this.Season = string.Empty;
            this.EpisodeBegin = string.Empty;
            this.EpisodeCount = -1;
        }
    }
}
