using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnglerNet.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public AspNetUsers User { get; set; }
    }
}
