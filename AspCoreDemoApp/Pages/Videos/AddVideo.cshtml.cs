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
    public class AddVideoModel : PageModel
    {
        private readonly IData<Channel> channelData;
       
        public Channel Channel { get; set; }

        [BindProperty]
        public Video Video { get; set; }
        public AddVideoModel(IData<Channel> channelData)
        {
            this.channelData = channelData;
        }

        public IActionResult OnGet(int channelId)
        {
            Video = new Video();
            Video.ChannelId = channelId;
            Video.Width = 640;
            Video.Height = 390;
            Channel = channelData.GetById(channelId);

            if(Channel == null)
            {
                return BadRequest();
            }

            return Page();
        }

        public IActionResult OnPost(int channelId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Channel = channelData.GetById(channelId);
            Channel.LastModified = DateTime.Now;
            Channel.Videos.Add(Video);

            channelData.Update(Channel);
            channelData.Commit();

            return RedirectToPage("./Index", new { id = Channel.Id });

        }
    }
}