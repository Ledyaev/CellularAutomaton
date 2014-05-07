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
        IEnumerable<User> IUserService.Get(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy, string includeProperties)
        {
            return unitOfWork.UsersRepository.Get(filter, orderBy, includeProperties);
        }

        User IUserService.GetById(string id)
        {
            return unitOfWork.UsersRepository.GetById(id);
        }

        void IUserService.Insert(User entity)
        {
            unitOfWork.UsersRepository.Insert(entity);
        }

        void IUserService.Delete(object id)
        {
            unitOfWork.UsersRepository.Delete(id);
        }

        void IUserService.Delete(User entityToDelete)
        {
            unitOfWork.UsersRepository.Delete(entityToDelete);
        }

        void IUserService.Update(User entityToUpdate)
        {
            unitOfWork.UsersRepository.Update(entityToUpdate);
        }

        void IUserService.Save()
        {
            unitOfWork.Save();
        }
    }
}
