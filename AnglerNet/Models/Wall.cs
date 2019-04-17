using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class Wall
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }

        public AspNetUsers User { get; set; }
    }
}
