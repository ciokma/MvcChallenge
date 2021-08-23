using hey_url_challenge_code.Resources;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code.Services
{
    public interface IUrlService
    {
        /// <summary>
        /// GenerateURL and save it
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CreateUrl(Url url);
        /// <summary>
        /// Gets all urls
        /// </summary>
        /// <returns></returns>
        List<Url> GetUrls();
        /// <summary>
        /// Gets Url object by ShortUrl
        /// </summary>
        /// <param name="shortUrl">here pass short url</param>
        /// <returns></returns>
        Url GetUrlByShortUrl(string shortUrl);
        /// <summary>
        /// Register click by url
        /// </summary>
        /// <param name="browser">browser</param>
        /// <param name="platform">platform</param>
        /// <param name="urlId">urlId (optional)</param>

        /// <returns></returns>
        bool CreateClicksByUrl(string browser, string platform, Guid? urlId = null);
        /// <summary>
        /// Gets all Clicks By Url
        /// </summary>
        /// <returns></returns>
        List<ClicksByUrl> GetClicksByUrl(Guid urlId);
        /// <summary>
        /// Get Url in a short format
        /// </summary>
        /// <returns></returns>
        string GenerateShortString();
        /// <summary>
        /// Validte wheter url exists in db
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        bool ValidateUrlExists(string originalUrl);
        /// <summary>
        /// Validate if an url is valid
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        bool IsAnUrlValid(string url);
    }
    public class UrlService : IUrlService
    {

        private readonly ApplicationContext _context;

        public UrlService()
        {

        }
        public UrlService(ApplicationContext context) => _context = context;


        private static readonly Random getrandom = new Random();
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static readonly int Base = Alphabet.Length;

        /// <summary>
        /// Create an URL
        /// </summary>
        /// <param name="url">Pass an Model of URL</param>
        /// <returns></returns>
        public bool CreateUrl(Url url)
        {
            _context.Urls.Add(url);
            _context.SaveChanges();
            return true;
        }
        /// <summary>
        /// Gets all urls order by descending
        /// </summary>
        /// <returns></returns>
        public List<Url> GetUrls()
        {
            return _context.Urls.Include(u => u.ClicksByUrls)
                .OrderByDescending(r=>r.CreateDate)
                .ToList();
        }
        /// <summary>
        /// Gets Url object by ShortUrl
        /// </summary>
        /// <param name="shortUrl">here pass short url</param>
        /// <returns></returns>
        public Url GetUrlByShortUrl(string shortUrl)
        {
            return _context.Urls.Where(u => u.ShortUrl.ToLower().Equals(shortUrl.ToLower())).FirstOrDefault<Url>();
        }
        /// <summary>
        /// Register click each time an user see an url
        /// </summary>
        /// <param name="urlId">Url Identificator</param>
        /// <param name="browser">Browser Name</param>
        /// <param name="platform">Platform Name</param>
        /// <returns></returns>
        public bool CreateClicksByUrl(string browser, string platform, Guid? urlId = null)
        {
            Guid urlIntId = Guid.Empty;
            if (urlId.HasValue)
            {
                Guid.TryParse(urlId.ToString(), out urlIntId);
                ClicksByUrl model = new ClicksByUrl
                {
                    UrlId = urlIntId,
                    Browser = browser,
                    Platform = platform,
                    CreateDate = DateTime.Now
                };
                _context.ClicksByUrls.Add(model);
                _context.SaveChanges();
                return true;

            }
            else
                return false;
        }
        /// <summary>
        /// Gets all Clicks By Url
        /// </summary>
        /// <returns></returns>
        public List<ClicksByUrl> GetClicksByUrl(Guid urlId)
        {
            return _context.ClicksByUrls.Where(u => u.UrlId == urlId).ToList();
        }
        /// <summary>
        /// Generate Short URL
        /// </summary>
        /// <returns>short string </returns>
        public string GenerateShortString()
        {
            int length = 5;
            if (length == 0)
            {
                return Alphabet[0].ToString();
            }
            string str = string.Empty;
            for (var i = 0; i < length; i++)
            {
                var random = getrandom.Next(1, Base);
                str += Alphabet[random];
            }

            return str.ToString().ToUpper();
        }

        public bool ValidateUrlExists(string originalUrl)
        {
            var result = _context.Urls.ToList().Where(row => row.OriginalUrl.Equals(originalUrl));
            return result.Count() > 0;
        }

        public bool IsAnUrlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result);
           
        }
    }
}
