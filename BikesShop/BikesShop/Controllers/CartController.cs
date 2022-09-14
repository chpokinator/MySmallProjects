using BikesShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BikesShop.Extensions;
using BikesShop.Models.ViewModels;

namespace BikesShop.Controllers
{
    public class CartController : Controller
    {
        BikesContext context;

        public CartController(BikesContext context)
        {
            this.context = context;
        }

        public IActionResult Index(string returnUrl)
        {
            var cart = GetCart();
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public IActionResult AddToCart(int BikeId, string returnUrl)
        {
            Bike bike = context.Bikes.FirstOrDefault(x => x.BikeId == BikeId);
            if (bike != null)
            {
                var cart = GetCart();
                cart.AddItem(bike, 1);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult RemoveFromCart(int BikeId, string returnUrl)
        {
            Bike bike = context.Bikes.FirstOrDefault(x => x.BikeId == BikeId);
            if (bike != null)
            {
                var cart = GetCart();
                cart.RemoveLine(bike);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return cart;
        }
    }
}
