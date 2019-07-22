using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using AspCoreDemoApp.Data;
using AspCoreDemoApp.Core;
using System.Linq;

namespace AspCoreDemoApp.Test
{
    
    public class VideoTests
    {
        
        [Fact]
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
                    Description = "",
                    code = "",
                    ImageUrl = "",
                    width = 740,
                    height = 315
                });

                context.Videos.Add(new Video()
                {
                    Id = 2,
                    ChannelId = 1,
                    Title = "An Evening with Paul Simon",
                    Description = "",
                    code = "",
                    ImageUrl = "",
                    width = 740,
                    height = 315
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
                    Description = "",
                    code = "",
                    ImageUrl = "",
                    width = 740,
                    height = 315
                });

                context.Videos.Add(new Video()
                {
                    Id = 2,
                    ChannelId = 1,
                    Title = "An Evening with Paul Simon",
                    Description = "",
                    code = "",
                    ImageUrl = "",
                    width = 740,
                    height = 315
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
    }
}
