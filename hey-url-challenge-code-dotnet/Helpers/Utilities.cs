using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code.Helpers
{
    public static class Utilities
    {
        #region helper's method
        /// <summary>
        /// validate if the string contains special characters
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public static bool HasSpecialChars(this string shortUrl)
        {
            return shortUrl
                .Where(ch => !Char.IsLetterOrDigit(ch))
                .Count() > 0;
        }
        /// <summary>
        /// Validate if exists an empty space
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public static bool HasEmptySpace(this string shortUrl)
        {
            return shortUrl
                .Where(ch => Char.IsWhiteSpace(ch))
                .Count() > 0;
        }

        #endregion
    }
}
