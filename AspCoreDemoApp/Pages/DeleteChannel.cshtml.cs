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
    public class DeleteChannelModel : PageModel
    {
        private readonly IData<Channel> channelData;

        public Channel Channel { get; set; }
        public DeleteChannelModel(IData<Channel> channelData)
        {
            this.channelData = channelData;
        }
        public IActionResult OnGet(int id)
        {
            Channel = channelData.GetById(id);
            if(Channel == null)
            {
                return BadRequest();
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var channel = channelData.Delete(id);
            channelData.Commit();
            if(channel == null)
            {
                return new StatusCodeResult(500);
            }

            TempData["Message"] = "Channel deleted!";
            return RedirectToPage("Index");

        }
    }
}