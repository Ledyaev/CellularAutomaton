using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CellularAutomaton.Domain.Interfaces
{
    public interface IMessage: IEntity
    {
        string Text { get; set; }

        [DataType(DataType.DateTime)]
        DateTime CreationDate { get; set; }

        bool IsRead { get; set; }

        ICellularAutomatonUser Recipient { get; set; }

        ICellularAutomatonUser Sender { get; set; }

    }
}
