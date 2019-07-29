﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreDemoApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IData<Video> videoData;

        public VideoController(IData<Video> videoData )
        {
            this.videoData = videoData;
        }

        [Route("next/{id}")]
        [HttpGet]
        public IActionResult GetNext(int id)
        {
            var video = videoData.GetById(id);
            if(video == null)
            {
                return BadRequest();
            }

            List<Video> videos = videoData.GetItems("").Where(v => v.ChannelId == video.ChannelId).ToList();
           
            int index = videos.FindIndex(v => v.Id == video.Id);

            if(index < videos.Count - 1)
            {
                //return next video in collection
                return Ok(videos[index + 1].Id);
            }
            else
            {
                //its the last video in collection so return first video
                return Ok(videos[0].Id);
            }
        }

        [Route("previous/{id}")]
        [HttpGet]
        public IActionResult GetPrevious(int id)
        {
            var video = videoData.GetById(id);
            if (video == null)
            {
                return BadRequest();
            }

            List<Video> videos = videoData.GetItems("").Where(v => v.ChannelId == video.ChannelId).ToList();

            int index = videos.FindIndex(v => v.Id == video.Id);

            if (index > 0)
            {
                //return previous video in collection
                return Ok(videos[index - 1].Id);
            }
            else
            {
                //its the first video in collection so return last video
                return Ok(videos[videos.Count - 1].Id);
            }
        }
    }
}