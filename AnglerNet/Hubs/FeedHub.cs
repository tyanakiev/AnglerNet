using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnglerNet.Models;
using Microsoft.AspNetCore.SignalR;

namespace AnglerNet.Hubs
{
    public class FeedHub: Hub
    {
        AnglerNetContext _context = new AnglerNetContext();
        public async Task SendMessage(string user, string sender, string message)
        {
            Profile currentProfile = _context.Profile.Where(o => o.UserId == sender).FirstOrDefault();
            await Clients.User(user).SendAsync("ReceiveMessage", user, currentProfile.Username, message);
            var test = Context.ConnectionId;
            //Feed newFeed = new Feed();
            //newFeed.SenderId = user;
        }
    }
}
