using System;
using System.Collections.Generic;
using System.Linq;
using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCoreDemoApp.Pages.Videos
{
    public class EditVideoModel : PageModel
    {
        private readonly IData<Channel> channelData;
        private readonly IData<Video> videoData;

        public Channel Channel { get; set; }

        [BindProperty]
        public Video Video { get; set; }
        public EditVideoModel(IData<Channel> channelData, IData<Video> videoData)
        {
            this.channelData = channelData;
            this.videoData = videoData;
        }

        public IActionResult OnGet(int id)
        {
            Video = videoData.GetById(id);

            if(Video == null)
            {
                return BadRequest();
            }

            Channel = channelData.GetById(Video.ChannelId);

            return Page();
        }

        public IActionResult OnPost(int channelId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            videoData.Update(Video);
            videoData.Commit();

            Channel = channelData.GetById(channelId);
            Channel.LastModified = DateTime.Now;
          
            channelData.Update(Channel);
            channelData.Commit();

            return RedirectToPage("./Index", new { id = Channel.Id });

        }
    }
}