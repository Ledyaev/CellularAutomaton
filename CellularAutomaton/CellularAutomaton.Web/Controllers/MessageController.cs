using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.Domain;
using CellularAutomaton.Web.Models;
using Microsoft.AspNet.Identity;

namespace CellularAutomaton.Web.Controllers
{
    public class MessageController : Controller
    {
        public MessageController(IMessageService messageService)
        {
            this.MessageService = messageService;
        }


        public IMessageService MessageService { get; set; }

        public ActionResult MyMessages()
        {
            
            ViewBag.Message = "Your contact page.";
            return View(User);
        }

        public ActionResult ChatRoom()
        {
            return View(User);
        }

        public ActionResult ShowDialogs()
        {
            var uId = User.Identity.GetUserId();
            var incomingMessages=MessageService.Get(m => m.Recipient.Id ==uId ,null,"").OrderByDescending(a => a.CreationDate);
            var dialogs = new Dictionary<String, int>();
            foreach (var message in incomingMessages)
            {
                if (dialogs.ContainsKey(message.Sender.UserName))
                {
                    dialogs[message.Sender.UserName]++;
                }
                else
                {
                    dialogs.Add(message.Sender.UserName, 1);
                }
            }
            return PartialView(dialogs);
        }
    }
}