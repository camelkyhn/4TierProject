using _4TierProject.Common.DataAccess;
using _4TierProject.Common.Entities;
using _4TierProject.DAL.RepositoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.BLL.Base
{
    public abstract class BaseRepository<T> : IDataAccess<T> where T : EntityBase
    {
        private Repository<T> repository;

        protected BaseRepository()
        {
            repository = new Repository<T>();
        }

        public bool Any(Expression<Func<T, bool>> _lamda)
        {
            return repository.Any(_lamda);
        }

        public IQueryable<T> Include(string searchString)
        {
            return repository.Include(searchString);
        }

        public int Delete(T entity)
        {
            return repository.Delete(entity);
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public T FindByExpression(Expression<Func<T, bool>> _lamda)
        {
            return repository.FindByExpression(_lamda);
        }

        public List<T> GetList()
        {
            return repository.GetList();
        }

        public List<T> GetList(Expression<Func<T, bool>> _lamda)
        {
            return repository.GetList(_lamda);
        }

        public IQueryable<T> GetListQueryable()
        {
            return repository.GetListQueryable();
        }

        public int Insert(T entity)
        {
            return repository.Insert(entity);
        }

        public int Save()
        {
            return repository.Save();
        }

        public int Update(T entity)
        {
            return repository.Update(entity);
        }
    }
}
