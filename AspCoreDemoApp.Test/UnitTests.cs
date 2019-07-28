using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using AspCoreDemoApp.Data;
using AspCoreDemoApp.Core;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using AspCoreDemoApp.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspCoreDemoApp.Test
{
    
    public class UnitTests
    {
        private readonly ITestOutputHelper output;

        public UnitTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        [Trait("Category", "EF")]
        public void GetItems_SearchTermIsJohnDenver_ReturnsVideosWithSearchTermInTitle()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<VideoDbContext>()
                .UseInMemoryDatabase($"VidoeDatabaseForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new VideoDbContext(options))
            {
                context.Channels.Add(new Channel()
                {
                    Id = 1,
                    Title = "Singer Songwriters",
                    Description = "A channel filled with singer songwriters",
                    LastModified = DateTime.Now
                });

                context.Videos.Add(new Video()
                {
                    Id = 1,
                    ChannelId = 1,
                    Title = "An Evening with John Denver",
                    Code = "",
                    Width = 740,
                    Height = 315
                });

                context.Videos.Add(new Video()
                {
                    Id = 2,
                    ChannelId = 1,
                    Title = "An Evening with Paul Simon",
                    Code = "",
                    Width = 740,
                    Height = 315
                });

                context.SaveChanges();
            }
            using (var context = new VideoDbContext(options))
            {
                SqlVideoData videoData = new SqlVideoData(context);

                //Act
                var videos = videoData.GetItems("John Denver");

                //Assert
                Assert.Single(videos);
            }
        }

        [Fact]
        [Trait("Category", "EF")]
        public void GetItems_EmptySearchTerm_ReturnsAllVideos()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<VideoDbContext>()
                .UseInMemoryDatabase($"VidoeDatabaseForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new VideoDbContext(options))
            {
                context.Channels.Add(new Channel()
                {
                    Id = 1,
                    Title = "Singer Songwriters",
                    Description = "A channel filled with singer songwriters",
                    LastModified = DateTime.Now
                });

                context.Videos.Add(new Video()
                {
                    Id = 1,
                    ChannelId = 1,
                    Title = "An Evening with John Denver",
                    Code = "",
                    Width = 740,
                    Height = 315
                });

                context.Videos.Add(new Video()
                {
                    Id = 2,
                    ChannelId = 1,
                    Title = "An Evening with Paul Simon",
                    Code = "",
                    Width = 740,
                    Height = 315
                });

                context.SaveChanges();
            }

            using (var context = new VideoDbContext(options))
            {
                SqlVideoData videoData = new SqlVideoData(context);

                //Act
                var videos = videoData.GetItems(null);

                //Assert
                Assert.Equal(2, videos.Count());
            }
        }

        [Fact]
        [Trait("Category", "EF")]
        public void Add_VideoWithoutTitle_ReturnsDbUpdateException()
        {
            //Arrange
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<VideoDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new VideoDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Channels.Add(new Channel()
                {
                    Id = 1,
                    Title = "Singer Songwriters",
                    Description = "A channel filled with singer songwriters",
                    LastModified = DateTime.Now
                });

                context.SaveChanges();

            }

            using (var context = new VideoDbContext(options))
            {
               
                SqlVideoData videoData = new SqlVideoData(context);
                var video = new Video()
                {
                    Id = 1,
                    ChannelId = 1,
                    Code = "",
                    Width = 740,
                    Height = 315
                };

                //Act
                videoData.Add(video);

                //Assert
                Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => videoData.Commit());
            }
        }


        [Fact]
        [Trait("Category", "EF")]
        public void Add_VideoWithoutValidChannelId_ReturnsDbUpdateException()
        {
            //Arrange

            //var logs = new List<string>();

            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<VideoDbContext>()
                .UseLoggerFactory( new LoggerFactory(
                    new[] { new LogToActionLoggerProvider((log) =>
                    {
                        //logs.Add(log);
                        this.output.WriteLine(log);  //writes to test explorer
                    })}))
                .UseSqlite(connection)
                .Options;

            using (var context = new VideoDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Channels.Add(new Channel()
                {
                    Id = 1,
                    Title = "Singer Songwriters",
                    Description = "A channel filled with singer songwriters",
                    LastModified = DateTime.Now
                });

                context.SaveChanges();

            }

            using (var context = new VideoDbContext(options))
            {
                SqlVideoData videoData = new SqlVideoData(context);
                var video = new Video()
                {
                    Id = 1,
                    ChannelId = 2,
                    Title = "This is a Title",
                    Code = "",
                    Width = 740,
                    Height = 315
                };

                //Act
                videoData.Add(video);

                //Assert
                Assert.Throws<DbUpdateException>(() => videoData.Commit());
            }
        }
    }
}
