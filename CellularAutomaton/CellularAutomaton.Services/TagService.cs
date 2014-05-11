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
    public class TagService: ITagService
    {
        private IUnitOfWork unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        IEnumerable<Tag> IService<Tag>.Get(Expression<Func<Tag, bool>> filter,
            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy, string includeProperties)
        {
            return unitOfWork.TagsRepository.Get(filter, orderBy, includeProperties);
        }

        Tag IService<Tag>.GetById(string id)
        {
            return unitOfWork.TagsRepository.GetById(id);
        }

        void IService<Tag>.Insert(Tag entity)
        {
            unitOfWork.TagsRepository.Insert(entity);
        }

        void IService<Tag>.Delete(object id)
        {
            unitOfWork.TagsRepository.Delete(id);
        }

        void IService<Tag>.Delete(Tag entityToDelete)
        {
            unitOfWork.TagsRepository.Delete(entityToDelete);
        }

        void IService<Tag>.Update(Tag entityToUpdate)
        {
            unitOfWork.TagsRepository.Update(entityToUpdate);
        }

        void IService<Tag>.Save()
        {
            unitOfWork.Save();
        }
    }
}
