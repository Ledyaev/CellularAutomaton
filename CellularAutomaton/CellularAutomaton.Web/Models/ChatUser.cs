using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;

namespace CellularAutomaton.Web.Models
{
    public class ChatUser
    {
        public string ConnectionId { get; set; }

        public string UserName { get; set; }
    }
}