using BikesShop.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BikesShop.Controllers.Api
{

    [ApiController]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BikesController : Controller
    {
        private readonly BikesContext context;
        public BikesController(BikesContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<IEnumerable<Bike>>> Get()
        {
            return await context.Bikes.ToListAsync();
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> Get(int id)
        {
            Bike bike = await context.Bikes.FirstOrDefaultAsync(x => x.BikeId == id);
            if (bike == null)
            {
                return NotFound();
            }
           
            return bike;
        }

        [HttpPost]
        public async Task<ActionResult<Bike>> Post(Bike bike)
        {
            if(bike.Title == "")
            {
                ModelState.AddModelError("Title", "Wrong title");
            }
            if(bike.Price < 1000)
            {
                ModelState.AddModelError("Price", "Too small price");
            }
            if(bike.WheelSize > 32)
            {
                ModelState.AddModelError("WheelSize", "too small wheels");
            }
            if(bike.Color == "")
            {
                ModelState.AddModelError("Color", "Wrong color");
            }
            if(bike.Material == "")
            {
                ModelState.AddModelError("Material", "Wrong material");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Bikes.Add(bike);
            await context.SaveChangesAsync();
            return Ok(bike);
        }


        [HttpPut]
        public async Task<ActionResult<Bike>> Put(Bike bike)
        {
            if (bike == null)
            {
                return BadRequest(ModelState);
            }
            if (!context.Bikes.Any(x => x.BikeId == bike.BikeId))
            {
                return NotFound();
            }
            if (bike.Title == "")
            {
                ModelState.AddModelError("Title", "Wrong title");
            }
            if (bike.Price < 1000)
            {
                ModelState.AddModelError("Price", "Wrong price");
            }
            if (bike.WheelSize > 32)
            {
                ModelState.AddModelError("WheelSize", "Wrong wheel size");
            }
            if (bike.Color == "")
            {
                ModelState.AddModelError("Color", "Wrong color");
            }
            if (bike.Material == "")
            {
                ModelState.AddModelError("Material", "Wrong material");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Update(bike);
            await context.SaveChangesAsync();
            return Ok(bike);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Bike>> Delete(int id)
        {
            Bike bike = context.Bikes.FirstOrDefault(x => x.BikeId == id);
            if (bike == null)
            {
                return NotFound();
            }
            context.Bikes.Remove(bike);
            await context.SaveChangesAsync();
            return Ok(bike);
        }
    }
}
