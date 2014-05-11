using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CellularAutomaton.Domain;

namespace CellularAutomaton.Web.Models
{
    public class AutomatonViewModel
    {

        public string Id { get; set; }

        public string Birth { get; set; }

        public string Overcrowding { get; set; }

        public string Loneliness { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string Area { get; set; }

        public string Discription { get; set; }

        public List<string> Tags { get; set; }

        public string CreatorName { get; set; }
    }
}