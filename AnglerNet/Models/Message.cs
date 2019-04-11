using System;
using System.Collections.Generic;

namespace AnglerNet.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int? ConversationId { get; set; }
        public string UserIdSend { get; set; }
        public string UserIdRecieve { get; set; }
        public string MsgBody { get; set; }
        public DateTime DateStamp { get; set; }
        public bool Seen { get; set; }

        public Conversation Conversation { get; set; }
        public AspNetUsers UserIdRecieveNavigation { get; set; }
        public AspNetUsers UserIdSendNavigation { get; set; }
    }
}
