using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain;

namespace CellularAutomaton.Services.Interfaces
{
    public interface IService<T> where T:IEntity
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties);

        T GetById(string id);

        void Insert(T entity);

        void Delete(object id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);

        void Save();
    }
}
