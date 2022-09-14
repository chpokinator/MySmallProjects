using BikesShop.Controllers.Api;
using BikesShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly BikesContext context;
        private readonly IdentityContext identityContext;
        private readonly int itemsPerPage = 7;
        public int TotalPages => (int)Math.Ceiling((decimal)context.Bikes.Count() / itemsPerPage);
        public AdminController(BikesContext context, IdentityContext identityContext, UserManager<Customer> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.identityContext = identityContext;
        }
        public static async Task<string> ConvertPhotoToBase64(IFormFile photo)
        {
            if(photo.Length > 0)
            {
                using(var ms = new MemoryStream())
                {
                    await photo.CopyToAsync(ms);
                    Byte[] bytes = ms.ToArray();
                    return $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
                }
            }
            return null;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Pages = TotalPages;
            return View(context.Bikes.OrderBy(x => x.BikeId).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList());
        }
        public IActionResult Creating(int? id)
        {
            var bike = context.Bikes.Find(id);
            return View(bike);
        }

        public IActionResult EditUsers()
        {
            return View(identityContext.Users.ToList());
        }

        public async Task<IActionResult> EditUser(string Id)
        {
            if(Id != null)
            {
                return View(identityContext.Users.Find(Id));
            }
            return View(await userManager.GetUserAsync(User));
            
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string email, string name, string password)
        {
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);
            user.Email = email;
            user.UserName = name;
            await userManager.ResetPasswordAsync(user, await userManager.GeneratePasswordResetTokenAsync(user), password);
            await userManager.UpdateAsync(user);
            return RedirectToAction("EditUsers");
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(string password)
        {
            var user = await userManager.GetUserAsync(User);
            await userManager.ResetPasswordAsync(user, await userManager.GeneratePasswordResetTokenAsync(user), password);
            await userManager.UpdateAsync(user);
            return Redirect("~/Home/Index");
        }


        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            var user = identityContext.Users.Find(id);
            if (user != null)
            {
                identityContext.Users.Remove(user);
                identityContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        //public IActionResult EditUserRole()
        //{
        //    //return View(context.Customers.Include(x => x.Role).ToList());
        //    return View(identityContext.Users.ToList());
            
        //} 
        
        //[HttpPost]
        //public IActionResult EditUserRole(int id, string roleString)
        //{
        //    Customer tmpCustomer = context.Customers.FirstOrDefault(x => x.Id == id);


        //    context.Entry(tmpCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    tmpCustomer.Role = context.Roles.FirstOrDefault(x => x.Name == roleString);
        //    tmpCustomer.RoleId = tmpCustomer.Role.Id;
        //    context.SaveChanges();

        //    return View(context.Customers.Include(x => x.Role).ToList());
        //}

        [HttpPost]
        public async Task<IActionResult> Creating(Bike bicycle, IFormFile uploadFile)
        {
            
            var entity = context.Bikes.Find(bicycle.BikeId);
            if(bicycle.BikeId == 0)
            {
                bicycle.Photo = await ConvertPhotoToBase64(uploadFile);
                context.Bikes.Add(bicycle);
            }
            else
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                entity.Color = bicycle.Color;
                entity.Material = bicycle.Material;
                entity.Price = bicycle.Price;
                entity.Title = bicycle.Title;
                entity.WheelSize = bicycle.WheelSize;
                entity.Info = bicycle.Info;
                entity.Photo = await ConvertPhotoToBase64(uploadFile);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var bicycle = context.Bikes.Find(id);
            if (bicycle != null)
            {
                context.Bikes.Remove(bicycle);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
