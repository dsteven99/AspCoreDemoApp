using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspCoreDemoApp.Core
{
    public  class Channel
    {
        public int Id { get; set; }
        [StringLength(75)]
        [Required]
        public string  Title { get; set; }
        public string Description { get; set; }
        public List<Video> Videos { get; set; }
        public DateTime LastModified { get; set; }
    }
}
