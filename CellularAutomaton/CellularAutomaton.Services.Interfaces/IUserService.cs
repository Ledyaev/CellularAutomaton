using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain;

namespace CellularAutomaton.Services.Interfaces
{
    public interface IUserService
    {

        IEnumerable<User> Get(Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy, string includeProperties);

        User GetById(string id);

        void Insert(User entity);

        void Delete(object id);

        void Delete(User entityToDelete);

        void Update(User entityToUpdate);

        void Save();
    }
}
