using System;
using System.Collections.Generic;

#nullable disable

namespace BlogsRepo.Models
{
    public partial class Blog
    {
        public string Id { get; set; }
        public string PreviewPhotoId { get; set; }
        public string Title { get; set; }
        public string InnerText { get; set; }
        public string AuthorId { get; set; }

        public virtual Photo PreviewPhoto { get; set; }
    }
}
