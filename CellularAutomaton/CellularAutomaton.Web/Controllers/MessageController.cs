using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.Domain;
using CellularAutomaton.Web.Filters;
using CellularAutomaton.Web.Models;
using Microsoft.AspNet.Identity;
using Ninject;

namespace CellularAutomaton.Web.Controllers
{
    [Culture]
    public class MessageController : Controller
    {

        [Inject]
        public IMessageService MessageService { get; set; }

        public ActionResult MyMessages()
        {
            
            ViewBag.Message = "Your contact page.";
            return View(User);
        }

        public ActionResult ShowDialogs()
        {
            var uId = User.Identity.GetUserId();
            var messages = MessageService.Get(m => m.Recipient.Id == uId || m.Sender.Id == uId, null, "").OrderByDescending(a => a.CreationDate);
            var dialogs = new Dictionary<String, int>();
            foreach (var message in messages)
            {
                if (message.Sender.Id != uId)
                {
                    if (message.IsRead == false)
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
                    else
                    {
                        if (!dialogs.ContainsKey(message.Sender.UserName))
                        {
                            dialogs.Add(message.Sender.UserName, 0);
                        }
                    }
                }
                else
                {
                    if (!dialogs.ContainsKey(message.Recipient.UserName))
                    {
                        dialogs.Add(message.Recipient.UserName, 0);
                    }
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
            ViewBag.UserName = userName;
            return View("ShowDialog", messages);
        }
    }
}