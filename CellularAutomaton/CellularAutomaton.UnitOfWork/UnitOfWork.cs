using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context;
using CellularAutomaton.Context.Interfaces;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories;
using CellularAutomaton.Repositories.Interfaces;
using CellularAutomaton.UnitOfWork.Interfaces;

namespace CellularAutomaton.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        public UnitOfWork(IRepository<User> usersRepository, IRepository<Message> messagesRepository, IContext context)
        {
            this.UsersRepository = usersRepository;
            this.MessagesRepository = messagesRepository;
            this.context = context;
        }

        private IContext context;

        public IRepository<User> UsersRepository { get; private set; }


        public IRepository<Message> MessagesRepository { get; private set; }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
