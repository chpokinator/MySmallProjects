using System;
using System.Collections.Generic;

#nullable disable

namespace BlogsRepo.Models
{
    public partial class Subscription
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SubId { get; set; }
    }
}
