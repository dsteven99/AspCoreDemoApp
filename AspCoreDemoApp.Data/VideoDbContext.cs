using AspCoreDemoApp.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDemoApp.Data
{
    public  class VideoDbContext : DbContext
    {
        public VideoDbContext(DbContextOptions<VideoDbContext> options)
         : base(options)
        {

        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Video> Videos { get; set; }
    }
}
