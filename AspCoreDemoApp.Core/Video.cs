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
        [StringLength(250)]
        [Required]
        public string code { get; set; }
        [Required]
        public int width { get; set; }
        [Required]
        public int height { get; set; }
        public string ImageUrl { get; set; }

    }
}
