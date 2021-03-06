﻿using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class Feed
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }

        public AspNetUsers Sender { get; set; }
        public AspNetUsers User { get; set; }
    }
}
