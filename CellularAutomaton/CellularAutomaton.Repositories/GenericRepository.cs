using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context;
using CellularAutomaton.Context.Interfaces;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories.Interfaces;

namespace CellularAutomaton.Repositories
{
    public class GenericRepository<TEntity>: IRepository<TEntity> where TEntity: IEntity
    {
        internal IContext context;
        internal DbSet dbSet;

        public GenericRepository(IContext context)
        {
            this.context = context;
            this.dbSet = context.Set(typeof(TEntity));
        }

        IEnumerable<TEntity> IRepository<TEntity>.Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            var query = (IQueryable<TEntity>)dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        TEntity IRepository<TEntity>.GetById(string id)
        {
            return (TEntity)dbSet.Find(id);
        }

        void IRepository<TEntity>.Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        void IRepository<TEntity>.Delete(object id)
        {
            var entityToDelete = (TEntity)dbSet.Find(id);
            ((IRepository<TEntity>)this).Delete(entityToDelete);
        }

        void IRepository<TEntity>.Delete(TEntity entityToDelete)
        {
            if (context.Entry((object)entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        void IRepository<TEntity>.Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry((object)entityToUpdate).State = EntityState.Modified;
        }
    }
}
