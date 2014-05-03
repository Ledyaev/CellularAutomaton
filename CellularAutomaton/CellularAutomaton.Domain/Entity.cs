using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton.Domain
{
    public class Entity: IEntity
    {
        string IEntity.Id { get; set; }
    }
}
