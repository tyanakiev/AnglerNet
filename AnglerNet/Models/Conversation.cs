using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            Message = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string UserIdOne { get; set; }
        public string UserIdTwo { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateLatest { get; set; }

        public AspNetUsers UserIdOneNavigation { get; set; }
        public AspNetUsers UserIdTwoNavigation { get; set; }
        public ICollection<Message> Message { get; set; }
    }
}
