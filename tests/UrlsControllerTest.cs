using hey_url_challenge_code.Services;
using hey_url_challenge_code_dotnet.Models;
using NUnit.Framework;
using System;
using Moq;
using Microsoft.EntityFrameworkCore;
using HeyUrlChallengeCodeDotnet.Data;
using System.Linq;
using System.Collections.Generic;
using hey_url_challenge_code.Helpers;

namespace tests
{
    public class UrlsControllerTest
    {
        private Url urlModel;
        private List<Url> listUrlModel;
        [SetUp]
        public void Setup()
        {
            urlModel = new Url
            {
                ShortUrl = "ABCDD",
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                OriginalUrl = "https://espanol.yahoo.com/?p=us"
            };
            listUrlModel = new List<Url>
            {
                new Url { Id = Guid.NewGuid(), ShortUrl = "amazon", OriginalUrl = "https://www.amazon.com/-/es/", Count = 1,CreateDate = DateTime.Now}
                ,new Url { Id = Guid.NewGuid(), ShortUrl = "youtube", OriginalUrl = "https://www.youtube.com/", Count = 1,CreateDate = DateTime.Now }
                ,new Url { Id = Guid.NewGuid(), ShortUrl = "dotnet5", OriginalUrl = "https://dotnet.microsoft.com/download/dotnet/5.0", Count = 1,CreateDate = DateTime.Now}
            };

        }
        /// <summary>
        /// save new url
        /// </summary>
        [Test]

        public void CreateUrl_Via_Context_SaveSuccessfull()
        {
            var mockSet = new Mock<DbSet<Url>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            service.CreateUrl(urlModel);

            mockSet.Verify(m => m.Add(It.IsAny<Url>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        /// <summary>
        /// save a click when visit a site
        /// </summary>
        [Test]
        public void RegisterClicksByUrl_Via_Context_SaveSuccessfull()
        {
            var mockSet = new Mock<DbSet<ClicksByUrl>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.ClicksByUrls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            service.CreateClicksByUrl("IE", "Window", urlModel.Id);

            mockSet.Verify(m => m.Add(It.IsAny<ClicksByUrl>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// - It MUST have 5 characters in length e.g.NELNT
        /// </summary>
        [Test]
        public void GenerateShortUrl_WhenSizeIsFive_ReturnedStringOfFiveChars()
        {
            var mockSet = new Mock<DbSet<Url>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var result = service.GenerateShortString();

            Assert.IsTrue(result.Length == 5);
        }
        /// <summary>
        /// - It MUST generate only upper case letters
        /// </summary>
        [Test]
        public void GenerateShortUrl_WhenAllLettersAreUpperCase_ReturnedFiveCharsInUpperCase()
        {
            var mockSet = new Mock<DbSet<Url>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var result = service.GenerateShortString();

            Assert.IsTrue(result.Equals(result.ToUpper()));
        }
        /// <summary>
        /// - It MUST NOT generate special characters
        /// </summary>
        [Test]
        public void GenerateShortUrl_WhenHasNotSpecialChars_Return5CharsWitoutSpecialChars()
        {
            var mockSet = new Mock<DbSet<Url>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var result = service.GenerateShortString();

            Assert.IsFalse(result.HasSpecialChars());
        }
        /// <summary>
        /// Validate if an url is valid
        /// </summary>
        /// <param name="wrongURL">wrong url</param>
        /// <param name="rightURL">right url</param>
        [Test]
        [TestCase("urlnotvalid", "https://www.google.com/")]
        public void ValidateURL_ReturnedTrue(string wrongURL, string rightURL)
        {
            var mockSet = new Mock<DbSet<Url>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var invalidURLResult = service.IsAnUrlValid(wrongURL);
            var rightURLResult = service.IsAnUrlValid(rightURL);

            Assert.IsFalse(invalidURLResult);
            Assert.IsTrue(rightURLResult);

        }
        /// <summary>
        /// - It MUST NOT generate whitespace
        /// </summary>
        [Test]
        public void GenerateShortUrl_WhenHasNotWhiteSpace_Return5CharsWitoutWhiteSpaces()
        {
            var mockSet = new Mock<DbSet<Url>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var result = service.GenerateShortString();

            Assert.IsFalse(result.HasSpecialChars());
        }
        /// <summary>
        /// - It MUST be unique
        /// </summary>
        [Test]
        public void GenerateShortUrl_Unique_Via_Context()
        {
            var mockSet = new Mock<DbSet<Url>>();
            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var resultFirstShortUrl = service.GenerateShortString();
            var resultSecondShortUrl = service.GenerateShortString();

            Assert.AreNotEqual(resultFirstShortUrl, resultSecondShortUrl);
        }
      
        /// <summary>
        /// - It MUST be unique
        /// </summary>
        [Test]
        public void GenerateShortUrl_WheterExistsURL_ReturnTrue()
        {
            urlModel.OriginalUrl = "https://dotnet.microsoft.com/download/dotnet/5.0";

            var data = listUrlModel.AsQueryable();
            var mockSet = new Mock<DbSet<Url>>();
            mockSet.As<IQueryable<Url>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Url>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Url>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Url>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var result = service.ValidateUrlExists(urlModel.OriginalUrl);

            Assert.IsTrue(result);
        }
        /// <summary>
        /// -Get all Urls available, validating
        /// </summary>
        [Test]
        public void GetAllUrls_WhenTotalIsThree_ReturnedList()
        {
            var data = listUrlModel.AsQueryable();
            var mockSet = new Mock<DbSet<Url>>();
            mockSet.As<IQueryable<Url>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Url>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Url>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Url>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var urls = service.GetUrls();

            Assert.AreEqual(3, urls.Count);
            Assert.AreEqual("amazon", urls[0].ShortUrl);
            Assert.AreEqual("youtube", urls[1].ShortUrl);
            Assert.AreEqual("dotnet5", urls[2].ShortUrl);
        }
        [Test]
        public void GetAllUrls_WhenThereIsNotData_ReturnedListEmpty()
        {
            var data = new List<Url>().AsQueryable();
            var mockSet = new Mock<DbSet<Url>>();
            mockSet.As<IQueryable<Url>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Url>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Url>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Url>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Urls).Returns(mockSet.Object);

            var service = new UrlService(mockContext.Object);
            var urls = service.GetUrls();

            Assert.AreEqual(0, urls.Count);
        }



    }
}