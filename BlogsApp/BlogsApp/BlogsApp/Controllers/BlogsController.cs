using BlogsApp.Models;
using BlogsRepo.Models;
using BlogsRepo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogsController : Controller
    {
        private readonly BlogsService blogsService;
        public BlogsController(BlogsService blogsService)
        {
            this.blogsService = blogsService;
        }

        [HttpGet("/getblogs")]
        public async Task<IActionResult> GetBlogs()
        {
            return Json(blogsService.GetBlogs());
        }

        [HttpGet("/photo")]
        public async Task<IActionResult> GetPhoto(string photoId)
        {
            var response = new
            {
                photobase64 = blogsService.GetPhotoBase64(photoId)
            };
            return Json(response);
        }

        [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/addphoto")]
        public async Task<IActionResult> AddPhoto(string photo)
        {
            Photo pic = new() { Photo1 = photo };
            blogsService.AddPhoto(pic);
            return Ok();
        }

        [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/getusersubs")]
        public async Task<IActionResult> GetBlogsSubs(string userId)
        {
            List<Subscription> tmp = blogsService.GetSubscriptions().Where(x => x.UserId == userId).ToList();

            List<Blog> blogsSubs = new List<Blog>();

            foreach(var sub in tmp)
            {
                blogsSubs.AddRange(blogsService.GetBlogsByUserId(sub.SubId));
            }

            return Json(blogsSubs);
        }

        [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/addblog")]
        public async Task<IActionResult> AddBlog([FromBody] NewBlog blog)
        {
            string photoId = Guid.NewGuid().ToString();
            blogsService.AddPhoto(new Photo { Photo1 = blog.Photo, Id = photoId });
            Blog newBlog = new() { AuthorId = blog.AuthorId, InnerText = blog.InnerText, Title = blog.Title, PreviewPhotoId = photoId, Id = "" };

            blogsService.CreateOrUpdateBlog(newBlog);

            return Ok();
        }

        [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/subscribe")]
        public async Task<IActionResult> Subscribe(string userId, string subId)
        {
            Subscription subscription = new() { UserId = userId, SubId = subId };
            blogsService.AddSubscription(subscription);

            return Ok();
        }


    }
}
