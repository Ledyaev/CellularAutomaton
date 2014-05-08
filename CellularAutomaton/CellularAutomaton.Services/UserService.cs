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
    public class UserService: IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        IEnumerable<User> IService<User>.Get(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy, string includeProperties)
        {
            return unitOfWork.UsersRepository.Get(filter, orderBy, includeProperties);
        }

        User IService<User>.GetById(string id)
        {
            return unitOfWork.UsersRepository.GetById(id);
        }

        void IService<User>.Insert(User entity)
        {
            unitOfWork.UsersRepository.Insert(entity);
        }

        void IService<User>.Delete(object id)
        {
            unitOfWork.UsersRepository.Delete(id);
        }

        void IService<User>.Delete(User entityToDelete)
        {
            unitOfWork.UsersRepository.Delete(entityToDelete);
        }

        void IService<User>.Update(User entityToUpdate)
        {
            unitOfWork.UsersRepository.Update(entityToUpdate);
        }

        void IService<User>.Save()
        {
            unitOfWork.Save();
        }
    }
}
