using System;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        /// <summary>
        /// Added : This will be the original URL
        /// </summary>
        public string OriginalUrl { get; set; }
        /// <summary>
        /// Added : This will be the creation date
        /// </summary>

        public DateTime CreateDate { get; set; }

        public int Count { get; set; }

        public virtual List<ClicksByUrl> ClicksByUrls { get; set; }

    }
}
