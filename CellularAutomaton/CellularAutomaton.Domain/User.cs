using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain.Interfaces;

namespace CellularAutomaton.Domain
{
    public class User: Entity, ICellularAutomatonUser
    {
        public string NickName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Avatar { get; set; }
        public string Thumbnail { get; set; }
        public string Icon { get; set; }
        public string ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual List<IMessage> IncomingMessages { get; set; }
        public virtual List<IMessage> OutgoingMessages { get; set; }
    }
}
