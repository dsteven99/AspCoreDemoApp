using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCoreDemoApp.Pages.Videos
{
    public class ScreenModel : PageModel
    {
        private readonly IData<Video> videoData;

        public ScreenModel(IData<Video> videoData)
        {
            this.videoData = videoData;
        }

        public Video Video { get; set; }
        public Channel Channel { get; set; }

        public IActionResult OnGet(int id)
        {
            Video = videoData.GetById(id);
            if(Video == null)
            {
                return BadRequest();
            }

            return Page();
        }
    }
}