using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Info { get; set; }
        public string PictureUrl { get; set; }

        public AspNetUsers User { get; set; }
    }
}
