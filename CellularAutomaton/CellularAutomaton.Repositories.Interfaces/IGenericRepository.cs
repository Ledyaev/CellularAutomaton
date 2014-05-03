using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain.Interfaces;

namespace CellularAutomaton.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: IEntity
    {
        //internal DbContext context;
        //internal IDbSet dbSet;
    }
}
