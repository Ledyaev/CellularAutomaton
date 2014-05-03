using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton.Domain
{
    public class Message: Entity
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsRead { get; set; }

        public virtual User Recipient { get; set; }

        public virtual User Sender { get; set; }
    }
}
