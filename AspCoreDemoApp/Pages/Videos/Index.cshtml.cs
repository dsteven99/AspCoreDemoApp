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
    public class IndexModel : PageModel
    {
        private readonly IData<Channel> channelData;
        private readonly IData<Video> videoData;

        public IEnumerable<Video> Videos { get; set; }
        public Channel Channel { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IndexModel(IData<Channel> channelData, IData<Video> videoData)
        {
            this.channelData = channelData;
            this.videoData = videoData;
        }
        public IActionResult OnGet(int id)
        {
            Channel = channelData.GetById(id);

            if (Channel == null)
            {
                return BadRequest();
            }

            Videos = videoData.GetItems(SearchTerm).Where(v => v.ChannelId == id);

            return Page();
        }
    }
}