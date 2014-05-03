using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain.Interfaces;

namespace CellularAutomaton.Domain
{
    public class Entity: IEntity
    {
        string IEntity.Id { get; set; }
    }
}
