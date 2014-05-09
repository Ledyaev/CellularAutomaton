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

        public ActionResult ShowDialogs()
        {
            var uId = User.Identity.GetUserId();
            var incomingMessages = MessageService.Get(m => m.Recipient.Id == uId || m.Sender.Id == uId, null, "").OrderByDescending(a => a.CreationDate);
            var dialogs = new Dictionary<String, int>();
            foreach (var message in incomingMessages)
            {
                if (message.Sender.Id == uId)
                {
                    
                }
                if (dialogs.ContainsKey(message.Sender.UserName))
                {
                    dialogs[message.Sender.UserName]++;
                }
                else
                {
                    dialogs.Add(message.Sender.UserName, 1);
                }
            }
            return PartialView( "_ShowDialogsPartial",dialogs);
        }

        public ActionResult ShowDialog(string userName)
        {
            var uId = User.Identity.GetUserId();
            var messages = MessageService
                .Get(m => (m.Recipient.Id == uId && m.Sender.UserName == userName) || (m.Sender.Id == uId && m.Recipient.UserName == userName), null, "").
                OrderByDescending(a => a.CreationDate);
            foreach (var message in messages)
            {
                if (message.Recipient.Id == uId && message.IsRead == false)
                {
                    message.IsRead = true;
                    MessageService.Update(message);
                }
            }
            MessageService.Save();
            ViewBag.UserName = userName;
            return View("ShowDialog", messages);
        }
    }
}