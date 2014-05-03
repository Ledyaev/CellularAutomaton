using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories;
using CellularAutomaton.Repositories.Interfaces;

namespace CellularAutomaton.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private CellularAutomatonContext context = new CellularAutomatonContext();

        private IRepository<User> usersRepository;

        private IRepository<Message> messagesRepository;

        public IRepository<User> UsersRepository
        {
            get
            {

                if (this.usersRepository == null)
                {
                    this.usersRepository = new GenericRepository<User>(context);
                }
                return usersRepository;
            }
        }


        public GenericRepository<Message> MessagesRepository
        {
            get
            {

                if (this.messagesRepository == null)
                {
                    this.messagesRepository = new GenericRepository<Message>(context);
                }
                return messagesRepository;
            }
        }

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
