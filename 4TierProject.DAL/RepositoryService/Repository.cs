using _4TierProject.Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.DAL.RepositoryService
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> entitySet;

        public Repository()
        {
            entitySet = context.Set<T>();
        }

        public bool Any(Expression<Func<T, bool>> _lamda)
        {
            return entitySet.Any(_lamda);
        }

        public int Delete(T entity)
        {
            entitySet.Remove(entity);
            return Save();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public T FindByExpression(Expression<Func<T, bool>> _lamda)
        {
            return entitySet.FirstOrDefault(_lamda);
        }

        public List<T> GetList()
        {
            return entitySet.ToList();
        }

        public List<T> GetList(Expression<Func<T, bool>> _lamda)
        {
            return entitySet.Where(_lamda).ToList();
        }

        public IQueryable<T> GetListQueryable()
        {
            return entitySet.AsQueryable<T>();
        }

        public int Insert(T entity)
        {
            entitySet.Add(entity);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Update(T entity)
        {
            entitySet.AddOrUpdate(entity);
            return Save();
        }
    }
}
