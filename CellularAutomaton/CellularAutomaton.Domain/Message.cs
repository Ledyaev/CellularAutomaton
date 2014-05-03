using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain.Interfaces;

namespace CellularAutomaton.Domain
{
    public class Message: IMessage
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRead { get; set; }
        public virtual ICellularAutomatonUser Recipient { get; set; }
        public virtual ICellularAutomatonUser Sender { get; set; }
    }
}
