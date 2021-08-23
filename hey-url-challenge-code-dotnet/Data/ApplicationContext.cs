using hey_url_challenge_code_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeyUrlChallengeCodeDotnet.Data
{
    public class ApplicationContext : DbContext
    {

        public virtual DbSet<Url> Urls { get; set; }
        public virtual DbSet<ClicksByUrl> ClicksByUrls { get; set; }
        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }


       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Url>()
                       .HasMany(b => b.ClicksByUrls)
                       .WithOne()
                       .HasPrincipalKey(b=>b.Id);

            modelBuilder.Entity<ClicksByUrl>()
                       .HasOne(p => p.Url)
                       .WithMany(b => b.ClicksByUrls)
                            .HasForeignKey(p => p.UrlId);

            modelBuilder.Entity<Url>()
              .Navigation(b => b.ClicksByUrls)
              .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }


}