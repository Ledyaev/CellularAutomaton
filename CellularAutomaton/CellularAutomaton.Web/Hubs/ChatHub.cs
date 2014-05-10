using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.Web.Models;
using CellularAutomaton.Domain;
using CellularAutomaton.Web.Scripts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using Ninject;

namespace CellularAutomaton.Web.Hubs
{
    public class ChatHub : Hub
    {
        static List<ChatUser> Users = new List<ChatUser>();
        [Inject]
        public IMessageService messageService { get; set; }
        [Inject]
        public IUserService userService { get; set; }

        //public ChatHub(IMessageService messageService, IUserService userService)
        //{
        //    this.userService = userService;
        //    this.messageService = messageService;
        //}
        // Отправка сообщений
        public void Send(string from ,string to, string text)
        {
            var userFrom = userService.Get(user => user.UserName == from, null, "").First();
            var userToId = Users.FirstOrDefault(x => x.UserName == to);
            var userTo = userService.Get(user => user.UserName == to, null, "").First();
            var message = new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Text = text,
                CreationDate = DateTime.Now,
                IsRead = false,
                Sender = userFrom,
                Recipient = userTo
            };
            messageService.Insert(message);
            var messages = messageService.Get(m => m.Recipient.Id == userFrom.Id &&m.Sender.Id == userTo.Id && m.IsRead == false, null, "");
            int unread = 0;
            foreach (var mes in messages)
            {
                unread++;
                    mes.IsRead = true;
                    messageService.Update(mes);
            }
            messageService.Save();
            Clients.Caller.addMessage(from, text,message.Id);
            if (userToId != null)
            {
                Clients.Client(userToId.ConnectionId).addMessage(from, text, message.Id);
                Clients.Client(userToId.ConnectionId).updateDialogs(from);
                Clients.Client(userToId.ConnectionId).updateUnreadMessages(1);
            }
            Clients.Caller.updateUnreadMessages(-unread);
            Clients.Caller.readMessages();
            //Clients.All.addMessage(name, message);
        }

        public void ReadMessage(string id)
        {
            var  message=messageService.Get(m => m.Id==id, null,"").FirstOrDefault();
            if (message != null)
            {
                message.IsRead = true;
                messageService.Update(message);
                messageService.Save();
                Clients.Caller.updateUnreadMessages(-1);
            }
        }


        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            if (Users.Count(x => x.ConnectionId == id) == 0)
            {
                Users.Add(new ChatUser { ConnectionId = id, UserName = userName });
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
            }
            return base.OnDisconnected();
        }
    }
}