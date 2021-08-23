using hey_url_challenge_code.Services;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HeyUrlChallengeCodeDotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBrowserDetection();
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "HeyUrl"));
            services.AddTransient<IUrlService, UrlService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationContext>();
            context.Database.EnsureCreated();
            AddTestData(context);

        }
        //seeding data
        private static void AddTestData(ApplicationContext context)
        {
            var testUrl1 = new Url
            {
                Id= Guid.NewGuid(),
                ShortUrl = "amazon",
                OriginalUrl = "https://www.amazon.com/-/es/",
                Count  = 1,
                CreateDate = DateTime.Now
            };

            context.Urls.Add(testUrl1);

            var testUrl2 = new Url
            {
                Id = Guid.NewGuid(),
                ShortUrl = "youtube",
                OriginalUrl = "https://www.youtube.com/",
                Count = 1,
                CreateDate = DateTime.Now
            };

            context.Urls.Add(testUrl2);

            var testUrl3 = new Url
            {
                Id = Guid.NewGuid(),
                ShortUrl = "yahoo",
                OriginalUrl = "https://espanol.yahoo.com/?p=us",
                Count = 1,
                CreateDate = DateTime.Now
            };

            context.Urls.Add(testUrl3);

            var testUrl4 = new Url
            {
                Id = Guid.NewGuid(),
                ShortUrl = "dotnet5",
                OriginalUrl = "https://dotnet.microsoft.com/download/dotnet/5.0",
                Count = 1,
                CreateDate = DateTime.Now
            };

            context.Urls.Add(testUrl4);

            var testUrl5 = new Url
            {
                Id = Guid.NewGuid(),
                ShortUrl = "sqlserver",
                OriginalUrl = "https://www.microsoft.com/es-co/download/details.aspx?id=101064",
                Count = 1,
                CreateDate = DateTime.Now
            };

            context.Urls.Add(testUrl5);

            context.SaveChanges();

            //Relation Amazon
            var modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl1.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05 ,01, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl1.Id,
                Browser = "Firefox",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);
            //Relation youtube

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl2.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05, 01, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl2.Id,
                Browser = "Firefox",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl2.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05, 06, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl2.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05, 06, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);

            //Relation yahoo

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl3.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05, 01, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl3.Id,
                Browser = "Firefox",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);

            //Relation dotnet5

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl4.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05, 01, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl4.Id,
                Browser = "Firefox",
                CreateDate = new DateTime(2021, 05, 01, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);


            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl4.Id,
                Browser = "IE",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "Windows"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl4.Id,
                Browser = "Firefox",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);

            //Relation sqlserver

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl5.Id,
                Browser = "Chrome",
                CreateDate = new DateTime(2021, 05, 01, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl5.Id,
                Browser = "Firefox",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "Ubuntu"
            };
            context.ClicksByUrls.Add(modelClick);

            modelClick = new ClicksByUrl
            {
                Id = Guid.NewGuid(),
                UrlId = testUrl5.Id,
                Browser = "Safari",
                CreateDate = new DateTime(2021, 05, 02, 23, 59, 59),
                Platform = "macOS"
            };
            context.ClicksByUrls.Add(modelClick);
            context.SaveChanges();

        }
    }
}
