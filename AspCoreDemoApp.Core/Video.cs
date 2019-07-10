using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspCoreDemoApp.Core
{
    public  class Video
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
        [StringLength(75)]
        [Required]
        public string Title { get; set; }
        public  string Description { get; set; }
        public string Url { get; set; }
    }
}
