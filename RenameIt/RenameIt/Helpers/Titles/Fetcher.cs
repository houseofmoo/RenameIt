using System;
using System.Collections.Generic;
using System.Linq;

namespace RenameIt.Helpers.Titles
{
    /// <summary>
    /// Fetch titles for a given show and season.
    /// </summary>
    public static class Fetcher
    {
        #region private constants
        /// <summary>
        /// URL to query the Show ID from API.
        /// </summary>
        public const string TvApiShowIdQueryUrl = @"http://api.tvmaze.com/singlesearch/shows?q=";

        /// <summary>
        /// URL to query the Episode Information from API.
        /// </summary>
        public const string TvApiEpisodeInfoQueryUrl = @"http://api.tvmaze.com/shows/";

        /// <summary>
        /// URL to assign the season of show we're looking for.
        /// </summary>
        public const string TvApiEpsodeSeasonQuery = @"episodebynumber?season=";

        /// <summary>
        /// URL to assign the episode we're looking for.
        /// </summary>
        public const string TvApiEpisodeNumberQuery = @"&number=";
        #endregion

        /// <summary>
        /// Returns a list of episode titles for the show based on season and starting point.
        /// </summary>
        /// <param name="showInfo">Information we need to make request to API.</param>
        /// <returns></returns>
        public static List<string> Get(Request showInfo)
        {
            // get show id
            string showId = fetchShowId(showInfo.ShowName);

            if (showId == null)
                return null;

            // get episode titles
            List<string> titles = fetchShowTitles(showId, showInfo.Season, showInfo.EpisodeBegin, showInfo.EpisodeCount);

            return titles;
        }

        /// <summary>
        /// Retreives the show id.
        /// </summary>
        /// <param name="showName">Name of show whose ID we're attempting to get.</param>
        /// <returns></returns>
        private static string fetchShowId(string showName)
        {
            try
            {
                // remove spaces replace with +
                showName = showName.Replace(" ", "+");

                // create url string from _showName
                string url = TvApiShowIdQueryUrl + showName;

                // make request and map reply to object
                ShowIdReply reply = null;
                using (var client = new System.Net.WebClient())
                {
                    var json = client.DownloadString(url);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    reply = serializer.Deserialize<ShowIdReply>(json);
                }

                // return item we care about
                return reply.id.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retreives episode titles using show id.
        /// </summary>
        /// <param name="showId">ID of show we're looking for.</param>
        /// <param name="season">Season number of show we're looking for.</param>
        /// <param name="epBegin">Which episode to begin with.</param>
        /// <param name="epCount">Number of episodes we are expecting to find.</param>
        /// <returns></returns>
        private static List<string> fetchShowTitles(string showId, string season, string epBegin, int epCount)
        {
            // list that will hold episode titles
            List<string> titles = new List<string>();

            try
            {
                // convert episode begin to int to incrementing through episodes
                int episodeBegin = Convert.ToInt32(epBegin);

                // make a request for each episodes title
                for (int i = 0; i < epCount; i++)
                {
                    // create url with show id, season, epBegin
                    string url = TvApiEpisodeInfoQueryUrl + showId + @"/" + TvApiEpsodeSeasonQuery +
                                 season + TvApiEpisodeNumberQuery + episodeBegin;

                    // make request and map reply to object
                    EpisodeInfoReply reply = null;
                    using (var client = new System.Net.WebClient())
                    {
                        var json = client.DownloadString(url);
                        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        reply = serializer.Deserialize<EpisodeInfoReply>(json);

                        titles.Add(reply.name);
                    }

                    // increment to next episode 
                    episodeBegin++;
                }

                // return item we care about
                return titles;
            }
            catch
            {
                if (titles != null && titles.Any())
                {
                    return titles;
                }
                return null;
            }
        }
    }
}
