using System;
using System.Collections.Generic;

#nullable disable

namespace BlogsRepo.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Blogs = new HashSet<Blog>();
        }

        public string Id { get; set; }
        public string Photo1 { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
