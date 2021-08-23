using System;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Models
{
    public class ClicksByUrl
    {
        public Guid Id { get; set; }
        public Guid UrlId { get; set; }
 
        public string Browser { get; set; }
        public string Platform { get; set; }

        public DateTime CreateDate { get; set; }
        public virtual Url Url { get; set; }

    }
}
