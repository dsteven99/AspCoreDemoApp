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
    public class EditChannelModel : PageModel
    {
        private readonly IData<Channel> channelData;

        [BindProperty]
        public Channel Channel { get; set; }

        public EditChannelModel(IData<Channel> channelData)
        {
            this.channelData = channelData;
        }

        public IActionResult OnGet(int id)
        {
            Channel = channelData.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Channel.LastModified = DateTime.Now;
            channelData.Update(Channel);

            channelData.Commit();

            TempData["Message"] = "Channel saved!";
            return RedirectToPage("Index");
        }
    }
}