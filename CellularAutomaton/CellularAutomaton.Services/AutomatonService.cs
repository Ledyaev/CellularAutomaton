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
    public class AutomatonService: IAutomatonService
    {
        private IUnitOfWork unitOfWork;

        public AutomatonService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        IEnumerable<Automaton> IService<Automaton>.Get(Expression<Func<Automaton, bool>> filter,
            Func<IQueryable<Automaton>, IOrderedQueryable<Automaton>> orderBy, string includeProperties)
        {
            return unitOfWork.AutomatonsRepository.Get(filter, orderBy, includeProperties);
        }

        Automaton IService<Automaton>.GetById(string id)
        {
            return unitOfWork.AutomatonsRepository.GetById(id);
        }

        void IService<Automaton>.Insert(Automaton entity)
        {
            unitOfWork.AutomatonsRepository.Insert(entity);
        }

        void IService<Automaton>.Delete(object id)
        {
            unitOfWork.AutomatonsRepository.Delete(id);
        }

        void IService<Automaton>.Delete(Automaton entityToDelete)
        {
            unitOfWork.AutomatonsRepository.Delete(entityToDelete);
        }

        void IService<Automaton>.Update(Automaton entityToUpdate)
        {
            unitOfWork.AutomatonsRepository.Update(entityToUpdate);
        }

        void IService<Automaton>.Save()
        {
            unitOfWork.Save();
        }
    }
}
