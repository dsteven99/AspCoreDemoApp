using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCoreDemoApp.Pages
{
    public class AddChannelModel : PageModel
    {
        private readonly IData<Channel> channelData;

        [BindProperty]
        public Channel Channel { get; set; }

        public AddChannelModel(IData<Channel> channelData)
        {
            this.channelData = channelData;
        }

        public IActionResult OnGet()
        {
            Channel = new Channel();
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Channel.LastModified = DateTime.Now;
            channelData.Add(Channel);

            channelData.Commit();

            return RedirectToPage("Index");
        }
    }
}