using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCoreDemoApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IData<Channel> channelData;

        public IEnumerable<Channel> Channels { get; set; }
        [TempData]
        public string Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IndexModel(IData<Channel> channelData)
        {
            this.channelData = channelData;
        }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                HttpContext.Session.SetString("ChannelSearchTerm", SearchTerm);
            }
            
            Channels = channelData.GetItems(SearchTerm);
            return Page();
        }
    }
}
