using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreDemoApp.Test
{
    public class Utilities
    {
        public static void InitializeDbForTests(VideoDbContext db)
        {
            // Seed the database with test data.
            db.Channels.Add(new Channel()
            {
                Id = 1,
                Title = "Singer Songwriters",
                Description = "A channel filled with singer songwriters",
                LastModified = DateTime.Now
            });

            db.Videos.Add(new Video()
            {
                Id = 1,
                ChannelId = 1,
                Title = "An Evening with John Denver",
                Code = "",
                Width = 740,
                Height = 315
            });

            db.Videos.Add(new Video()
            {
                Id = 2,
                ChannelId = 1,
                Title = "An Evening with Paul Simon",
                Code = "",
                Width = 740,
                Height = 315
            });

            db.SaveChanges();
        }
    }
}
