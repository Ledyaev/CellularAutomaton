using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CellularAutomaton.Web.Models;
using CellularAutomaton.Domain;
using CellularAutomaton.Web.Scripts;
using Microsoft.AspNet.SignalR;

namespace CellularAutomaton.Web.Hubs
{
    public class ChatHub : Hub
    {
        static List<TempUser> Users = new List<TempUser>();

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.Client("fa2b3c08-3af9-4020-bfca-4596f87cba2e").addMessage(name, message);
            //Clients.Client().addMessage(name, message);
        }

        // Подключение нового пользователя
        public void Connect()
        {
            var id = Context.ConnectionId;

            if (Users.Count(x => x.ConnectionId == id) == 0)
            {
                Users.Add(new TempUser { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected();
        }
    }
}