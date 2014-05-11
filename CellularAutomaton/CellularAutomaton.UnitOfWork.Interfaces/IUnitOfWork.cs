using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories.Interfaces;

namespace CellularAutomaton.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }

        IRepository<Message> MessagesRepository { get; }

        IRepository<Automaton> AutomatonsRepository { get; }

        IRepository<Tag> TagsRepository { get; } 

        void Save();
    }
}
