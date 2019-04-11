using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class Relationship
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public int OrderNumber { get; set; }

        public AspNetUsers Friend { get; set; }
        public AspNetUsers User { get; set; }
    }
}
