using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton.Domain
{
    public class Automaton: IEntity
    {
        public string Id { get; set; }

        public string Rules { get; set; }

        public string Area { get; set; }

        public string Description { get; set; }

        public virtual List<Tag> Tags { get; set; }

        public virtual User Creator { get; set; }
    }
}
