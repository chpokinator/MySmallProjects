using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesShop.Models
{
    public class BikesContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public BikesContext(DbContextOptions<BikesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
       
    }
}
