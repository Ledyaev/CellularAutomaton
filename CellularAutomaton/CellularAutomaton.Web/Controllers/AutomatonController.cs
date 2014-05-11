using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using CellularAutomaton.Domain;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.Web.Models;
using Microsoft.AspNet.Identity;
using Ninject;

namespace CellularAutomaton.Web.Controllers
{
    public class AutomatonController : Controller
    {
        [Inject]
        public IAutomatonService AutomatonService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public ITagService TagService { get; set; }

        [HttpPost]
        public ActionResult Save(string area, string rules, string discription, string tags, string name)
        {
            var user = UserService.GetById(User.Identity.GetUserId());
            var userFolder = GetUserFolder();
            var filename = String.Format("{0}{1}", area, rules).GetHashCode();
            var filepath = String.Format("{0}/{1}.json", userFolder, filename);
            if (System.IO.File.Exists(Server.MapPath(filepath)))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            System.IO.File.WriteAllText(Server.MapPath(filepath),area);
            var automaton = new Automaton()
            {
                Name = name,
                Area = filepath,
                Creator = user,
                Discription = discription,
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Rules = rules
            };
            var tagsList = ParseTags(tags);
            foreach (var tag in tagsList)
            {
                var foundTag = SearchTag(tag);
                foundTag.Automatons.Add(automaton);
                if (foundTag.Id == null)
                {
                    foundTag.Id = Guid.NewGuid().ToString();
                    TagService.Insert(tag);
                }
                else
                {
                    TagService.Update(foundTag);
                }
            }
            TagService.Save();
            AutomatonService.Save();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowAutomaton(string id)
        {
            var automaton = AutomatonService.GetById(id);
            var rules = automaton.Rules.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var automatonModel = new AutomatonViewModel()
            {
                Birth = rules[0],
                Overcrowding = rules[1],
                Loneliness = rules[2],
                Id = automaton.Id,
                Name = automaton.Name,
                CreationDate = automaton.CreationDate,
                Area = System.IO.File.ReadAllText(Server.MapPath(automaton.Area)),
                Discription = automaton.Discription,
                Tags = automaton.Tags.Select(t => t.Name).ToList(),
                CreatorName = automaton.Creator.UserName
            };
            return View(automatonModel);
        }


        public ActionResult SearchByTag(string tag)
        {
            var findTag = TagService.Get(t => t.Name == tag, null, "").FirstOrDefault();
            return View("asdf", findTag.Automatons);
        }

        public ActionResult UserAutomatons(string id)
        {
            var user = UserService.GetById(id);
            //List<Automaton> automatons=null;
            //if (user != null)
            //{
            //    automatons = user.Automatons;
            //}
            return View("UserAutomatons", user);
        }

        public ActionResult ShowAutomatons()
        {
            var automatons = AutomatonService.Get(null, q => q.OrderByDescending(a => a.CreationDate), "").ToList();
            return PartialView("_JustAddedPartial",automatons);
        }

        public ActionResult Galery()
        {
            return View();
        }

        public ActionResult JustAdded()
        {
            var automatons = AutomatonService.Get(null,q => q.OrderByDescending(a => a.CreationDate),"").ToList();
            if (automatons.Count > 3)
            {
                automatons = automatons.Take(3).ToList();
            }
            return PartialView("_JustAddedPartial", automatons);
        }


        public ActionResult NewAutomaton()
        {
            return View();
        }

        private string GetUserFolder()
        {
            if (!Directory.Exists(Server.MapPath("~/Automatons")))
                Directory.CreateDirectory(Server.MapPath("~/Automatons"));
            string userFolder = "~/Automatons/" + User.Identity.Name.GetHashCode();
            if (!Directory.Exists(Server.MapPath(userFolder)))
                Directory.CreateDirectory(Server.MapPath(userFolder));
            return userFolder;
        }

        private IEnumerable<Tag> ParseTags(string tagsString)
        {
            var tags = tagsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tags.Count(); i++)
            {
                tags[i] = tags[i].ToLower();
            }
            return tags.Select(tag=> new Tag(){Name = tag,Automatons = new List<Automaton>()});
        }


        private Tag SearchTag(Tag tag)
        {
            var existedTag = TagService.Get(t=>t.Name==tag.Name,null,"").FirstOrDefault();
            return existedTag ?? tag;
        }
    }
}