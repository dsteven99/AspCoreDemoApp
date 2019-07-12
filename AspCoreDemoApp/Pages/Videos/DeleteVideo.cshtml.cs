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
    public class DeleteVideoModel : PageModel
    {
        private readonly IData<Video> videoData;

        public Video Video { get; set; }

        public DeleteVideoModel(IData<Video> videoData)
        {
            this.videoData = videoData;
        }
        public IActionResult OnGet(int id)
        {
            Video = videoData.GetById(id);

            if(Video == null)
            {
                return BadRequest();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var video = videoData.Delete(id);
            videoData.Commit();

            if(video == null)
            {
                return new StatusCodeResult(500);
            }

            TempData["Message"] = "Video deleted!";
            return RedirectToPage("./Index", new { id = video.ChannelId });
        }
    }
}