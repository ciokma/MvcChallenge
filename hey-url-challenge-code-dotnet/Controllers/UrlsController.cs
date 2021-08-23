using System;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using System.Linq;
using Microsoft.AspNetCore.Http;
using hey_url_challenge_code.Resources;
using hey_url_challenge_code.Services;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IUrlService _urlServices;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IUrlService urlServices)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _urlServices = urlServices;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();

            var urls = _urlServices.GetUrls();

            var Urls = new List<Url>();
            urls.ToList().ForEach(row =>
            {
                Url url = new Url()
                {
                    Id = row.Id,
                    ShortUrl = row.ShortUrl,
                    OriginalUrl = row.OriginalUrl,
                    CreateDate = row.CreateDate,
                    Count = row.ClicksByUrls.Count
                };
                Urls.Add(url);
            });
            model.Urls = Urls.OrderByDescending(d => d.CreateDate).ToList();
            model.NewUrl = new();
            @ViewData["IsSavedSuccessfulUrl"] = false;

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            var model = new HomeViewModel();
            model.Urls = new List<Url>();
            try
            {
                var longUrl = collection["longUrl"];
                //validating the url
                if (!_urlServices.IsAnUrlValid(longUrl))
                {
                    throw new Exception(Language.UrlNotValid);
                }
                //validate url exists?
                bool existUrl = _urlServices.ValidateUrlExists(longUrl.ToString().Trim());
                if (existUrl)
                {
                    throw new Exception(Language.UrlExist);
                }

                Url url = new Url
                {
                    ShortUrl = _urlServices.GenerateShortString(),
                    Count = 0,
                    CreateDate = DateTime.Now,
                    OriginalUrl = longUrl
                };
                //let's create the short url
                _urlServices.CreateUrl(url);
                @ViewData["Info"] = "Data saved successfully!";
                @ViewData["IsUrlSavedsuccessfully"] = true;
                @ViewData["shortUrl"] = url.ShortUrl;

                model.Urls = _urlServices.GetUrls();
                @ViewBag.IsError = false;
                return View("index", model);
            }
            catch (Exception ex)
            {
                model.Urls = _urlServices.GetUrls();
                @ViewBag.IsError = true;
                @ViewData["Info"] = ex.Message;
                return View("index", model);

            }

        }

        [Route("/{url}")]
        public IActionResult Visit(string url) 
        {
            try
            {
                var GetUrl = _urlServices.GetUrlByShortUrl(url);
                AgentViewModel agent = new AgentViewModel();
                //Increment the counter
                if (_urlServices.CreateClicksByUrl(this.browserDetector.Browser.OS, this.browserDetector.Browser.Name, GetUrl?.Id))
                {
                    agent.Platform = this.browserDetector.Browser.OS;
                    agent.Browser = this.browserDetector.Browser.Name;
                    agent.ShortUrl = url;
                    agent.OriginalUrl = _urlServices.GetUrlByShortUrl(url).OriginalUrl;
                    return View(agent);
                }
                return View("CustomError", new { url = url });

            }
            catch
            {
                return View("CustomError", new { url = url });
            }
        }
        [HttpGet]
        [Route("CustomError/{url}")]
        public IActionResult CustomError(string url)
        {
            return View();
        }

        
        [Route("urls/{url}")]
        public IActionResult Show(string url)
        {
            var model = new ShowViewModel();
            var result = _urlServices.GetUrlByShortUrl(url);
            var clicks = _urlServices.GetClicksByUrl(result.Id);

            if (clicks.Count == 0)
            {
                model.DailyClicks = GetEmptyObjectDictionary();
                model.BrowseClicks = GetEmptyObjectDictionary();
                model.PlatformClicks = GetEmptyObjectDictionary();
            }
            else
            {
                //grouping  and counting by day
                var DailyClicks = from x in clicks
                                  let day = x.CreateDate.Day
                                  group x by day into g
                                  select new
                                  {
                                      Day = g.Key,
                                      Count = g.Count()
                                  };
                //grouping  and counting  by browser
                var BrowseClicks = from x in clicks
                                   group x by x.Browser into g
                                   select new
                                   {
                                       Browser = g.Key,
                                       Count = g.Count()
                                   };
                //grouping  and counting by platform
                var PlatformClicks = from x in clicks
                                     group x by x.Platform into g
                                     select new
                                     {
                                         Platform = g.Key,
                                         Count = g.Count()
                                     };

                model.DailyClicks = DailyClicks.ToDictionary(x => x.Day.ToString(), x => x.Count);
                model.BrowseClicks = BrowseClicks.ToDictionary(x => x.Browser.ToString(), x => x.Count);
                model.PlatformClicks = PlatformClicks.ToDictionary(x => x.Platform.ToString(), x => x.Count);
            }
            model.Url = result;

            return View(model);
        }
        private Dictionary<string, int> GetEmptyObjectDictionary()
        {
            return new Dictionary<string, int>
                {
                            {"0", 0}
                };
        }

    }
}