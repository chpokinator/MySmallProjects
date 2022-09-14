using BlogsRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsRepo.Services
{
    public class BlogsService
    {
        private BlogsAppDBContext context = new BlogsAppDBContext();


        public IEnumerable<Blog> GetBlogs()
        {
            return context.Blogs.ToList();
        }

        public IEnumerable<Blog> GetBlogsByUserId(string userId)
        {
            return context.Blogs.Where(x => x.AuthorId == userId);
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return context.Photos.ToList();
        }

        public string GetPhotoBase64(string id)
        {
            Photo photo = context.Photos.FirstOrDefault(x => x.Id == id);
            if (photo == null)
            {
                return "no photo";
            }
            return photo.Photo1;



        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return context.Subscriptions.ToList();
        }

        public void CreateOrUpdateBlog(Blog blog)
        {
            if (blog.Id == "")
            {
                blog.Id = Guid.NewGuid().ToString();
                context.Blogs.Add(blog);
            }
            else
            {
                var entity = context.Blogs.Find(blog.Id);
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                entity.Title = blog.Title;
                entity.PreviewPhotoId = blog.PreviewPhotoId;
                entity.InnerText = blog.InnerText;
                entity.PreviewPhotoId = blog.PreviewPhotoId;
                entity.AuthorId = blog.AuthorId;
            }
            context.SaveChanges();
        }

        public void AddPhoto(Photo photo)
        {
            context.Photos.Add(photo);
            context.SaveChanges();
        }

        public void AddSubscription(Subscription subscription)
        {
            subscription.Id = Guid.NewGuid().ToString();
            context.Subscriptions.Add(subscription);
        }

        public void DeleteSubcription(Subscription subscription)
        {
            context.Subscriptions.Remove(subscription);
            context.SaveChanges();
        }

    }
}
