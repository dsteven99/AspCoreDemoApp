using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCoreDemoApp.Pages.Videos
{
    public class ScreenModel : PageModel
    {
        private readonly IData<Video> videoData;

        public string VideoSearchTerm { get; set; }

        public ScreenModel(IData<Video> videoData)
        {
            this.videoData = videoData;
        }

        [BindProperty]
        public string VideoID { get; set; }

        public int ChannelID { get; set; }
       
        public IActionResult OnGet(int id)
        {
            var Video = videoData.GetById(id);
            if(Video == null)
            {
                return BadRequest();
            }

            VideoID = Video.Code;

            ChannelID = Video.ChannelId;

            VideoSearchTerm = HttpContext.Session.GetString("VideoSearchTerm");

            return Page();
        }
    }
}