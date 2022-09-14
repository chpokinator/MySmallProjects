using BikesShop.Models;
using BikesShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly BikesContext context;
        private readonly int itemsPerPage = 2;
        public int TotalPages => (int)Math.Ceiling((decimal)context.Bikes.Count() / itemsPerPage);
        

        public HomeController(BikesContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.Pages = TotalPages;
            var materialListItems = new List<string>
            {
                "all"
            };
            var colorListItems = new List<string>
            {
                "all"
            };
            materialListItems.AddRange(context.Bikes.Select(x => x.Material).Distinct());
            colorListItems.AddRange(context.Bikes.Select(x => x.Color).Distinct());
            return View(new BikesListViewModel
            {
                Colors = new SelectList(colorListItems),
                Materials = new SelectList(materialListItems)
            });


        }

        public IActionResult BikesList(string content, int page = 1, string material = "all", string color = "all", string search = "all", string sortType = "tAsc")
        {
            
            ViewBag.Content = content;
            var materialListItems = new List<string>
            {
                "all"
            };
            var colorListItems = new List<string>
            {
                "all"
            };
            materialListItems.AddRange(context.Bikes.Select(x => x.Material).Distinct());
            colorListItems.AddRange(context.Bikes.Select(x => x.Color).Distinct());
            List<Bike> bikes = context.Bikes.ToList();
            if (color != "all" || material != "all")
            {
                if (color != "all")
                {
                    bikes = bikes.Where(x => x.Color.ToLower() == color.ToLower()).ToList();
                }
                if (material != "all")
                {
                    bikes = bikes.Where(x => x.Material.ToLower() == material.ToLower()).ToList();
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                bikes = bikes.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            //Bikes = context.Bikes.OrderBy(x => x.BikeId).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList()

            switch (sortType)
            {
                case "tAsc":
                    bikes = bikes.OrderBy(x => x.Title).ToList();
                    break;
                case "pAsc":
                    bikes = bikes.OrderBy(x => x.Price).ToList();
                    break;

            }



            return PartialView(new BikesListViewModel
            {
                Bikes = bikes.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                Colors = new SelectList(colorListItems),
                Materials = new SelectList(materialListItems),
                Pages = TotalPages,
                SortType = sortType
            });
        }


        public IActionResult Order(int? id)
        {
            if (id == null && id < 0 && id >= context.Bikes.Count())
                return RedirectToAction("Index");

            return View(context.Bikes.Find((int)id));
        }
        [HttpPost]
        public IActionResult Order(int BikeId, string Name, int Count, string Email)
        {



            Order order = new Order()
            {
                BikeId = BikeId,
                Bike = context.Bikes.Find(BikeId),
                Count = Count,
                TotalSum = context.Bikes.Find(BikeId).Price * Count,
                Email = Email
            };

            context.Orders.Add(order);
            context.SaveChanges();

            string massage = $"Thank you for purchasing bicycle {Name}. Message was sent automatically";
            Services.MailManager.SendMail(massage, Email);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
