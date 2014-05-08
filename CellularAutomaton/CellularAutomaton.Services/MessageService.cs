using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.UnitOfWork.Interfaces;

namespace CellularAutomaton.Services
{
    public class MessageService:IMessageService
    {
        private IUnitOfWork unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        IEnumerable<Message> IService<Message>.Get(Expression<Func<Message, bool>> filter, Func<IQueryable<Message>, IOrderedQueryable<Message>> orderBy, string includeProperties)
        {
            return unitOfWork.MessagesRepository.Get(filter, orderBy, includeProperties);
        }

        Message IService<Message>.GetById(string id)
        {
            return unitOfWork.MessagesRepository.GetById(id);
        }

        void IService<Message>.Insert(Message entity)
        {
            unitOfWork.MessagesRepository.Insert(entity);
        }

        void IService<Message>.Delete(object id)
        {
            unitOfWork.MessagesRepository.Delete(id);
        }

        void IService<Message>.Delete(Message entityToDelete)
        {
            unitOfWork.MessagesRepository.Delete(entityToDelete);
        }

        void IService<Message>.Update(Message entityToUpdate)
        {
            unitOfWork.MessagesRepository.Update(entityToUpdate);
        }

        void IService<Message>.Save()
        {
            unitOfWork.Save();
        }
    }
}
