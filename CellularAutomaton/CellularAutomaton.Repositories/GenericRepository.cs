using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories.Interfaces;

namespace CellularAutomaton.Repositories
{
    public class GenericRepository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        internal DbContext context;
        //internal DbSet dbSet;

        public GenericRepository(CellularAutomatonContext context)
        {
            this.context = context;
            //this.dbSet = context.Set(typeof(TEntity));
        }



        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            var query = (IQueryable<TEntity>)context.Set<TEntity>();

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

        public TEntity GetById(string id)
        {
            return (TEntity)context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delete(object id)
        {
            var entityToDelete = (TEntity)context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entityToDelete);
            }
            context.Set<TEntity>().Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            context.Set<TEntity>().Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
