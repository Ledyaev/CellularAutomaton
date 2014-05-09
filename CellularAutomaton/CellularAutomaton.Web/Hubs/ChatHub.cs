using System;
using System.Collections.Generic;
using System.Linq;
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
            var userFromId = Users.FirstOrDefault(x => x.UserName == from);
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
            messageService.Save();
            Clients.Caller.addMessage(from, text);
            Clients.Client(userToId.ConnectionId).addMessage(from, text);
            Clients.Client(userToId.ConnectionId).updateDialogs(from);
            Clients.Client(userToId.ConnectionId).updateUnreadMessages();
            //Clients.All.addMessage(name, message);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            if (Users.Count(x => x.ConnectionId == id) == 0)
            {
                Users.Add(new ChatUser { ConnectionId = id, UserName = userName });
                //// Посылаем сообщение текущему пользователю
                //Clients.Caller.onConnected(id, userName, Users);

                //// Посылаем сообщение всем пользователям, кроме текущего
                //Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                //var id = Context.ConnectionId;
                //Clients.All.onUserDisconnected(id, item.UserName);
            }

            return base.OnDisconnected();
        }
    }
}