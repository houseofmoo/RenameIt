using System;
using System.Collections.Generic;
using System.Linq;

namespace RenameIt.Helpers
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
        /// Returns a list of episode titles for the show based on season and starting point. Returns null if request failed.
        /// </summary>
        /// <param name="showInfo">Information we need to make request to API.</param>
        /// <returns></returns>
        public static List<string> GetEpisodeTitles(Models.Titles.Request showInfo)
        {
            // get show id
            string showId = fetchShowId(showInfo.ShowName);

            // if getting show id failed, no reason to continue
            if (showId == null)
                return null;

            // get episode titles
            List<string> titles = fetchShowTitles(showId, showInfo);

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
                Models.Titles.ShowIdReply reply = null;
                using (var client = new System.Net.WebClient())
                {
                    var json = client.DownloadString(url);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    reply = serializer.Deserialize<Models.Titles.ShowIdReply>(json);
                }

                // return info we care about
                return reply.id.ToString();
            }
            catch (Exception e)
            {
                // debugging
                Console.WriteLine(e);
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
        private static List<string> fetchShowTitles(string showId, Models.Titles.Request showInfo)
        {
            // list that will hold episode titles
            List<string> titles = new List<string>();

            try
            {
                // convert episode begin to int to incrementing through episodes
                int episodeBegin = Convert.ToInt32(showInfo.EpisodeBegin);

                // create new webclient for api requests 
                using (var client = new System.Net.WebClient())
                {
                    // make a request for each episodes title
                    for (int i = 0; i < showInfo.EpisodeCount; i++)
                    {
                        // create url with show id, season, epBegin
                        string url = TvApiEpisodeInfoQueryUrl + showId + @"/" + TvApiEpsodeSeasonQuery +
                                     showInfo.Season + TvApiEpisodeNumberQuery + episodeBegin;

                        // make request and map reply to object
                        var json = client.DownloadString(url);
                        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        var reply = serializer.Deserialize<Models.Titles.EpisodeInfoReply>(json);
                        
                        if (reply != null)
                            titles.Add(reply.name);

                        // increment to next episode 
                        episodeBegin++;
                    }
                }

                // return item we care about
                return titles;
            }
            catch
            {
                // occurs when the api request was faulty
                if (titles != null && titles.Any())
                    // return titles we may have received
                    return titles;

                return null;
            }
        }
    }
}
