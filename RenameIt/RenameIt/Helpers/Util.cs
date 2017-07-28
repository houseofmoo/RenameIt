using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameIt.Helpers
{
    public static class Util
    {
        public static bool ValidateInput(string showName, string season, string episodeStartNumber, int count)
        {
            try
            {
                // attempt to convert values to numbers
                var s = Convert.ToInt32(season);
                var e = Convert.ToInt32(episodeStartNumber);
            }
            catch
            {
                // if that failed, we return false
                return false;
            }

            // return ture if each text box is filled and count > 0
            return !string.IsNullOrWhiteSpace(showName) &&
                !string.IsNullOrWhiteSpace(season) &&
                !string.IsNullOrWhiteSpace(episodeStartNumber) &&
                count > 0;
        }
    }
}
