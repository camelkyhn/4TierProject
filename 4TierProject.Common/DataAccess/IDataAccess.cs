using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.DataAccess
{
    public interface IDataAccess<T>
    {
        int Insert(T entity);
        int Delete(T entity);
        int Update(T entity);
        int Save();
        List<T> GetList();
        IQueryable<T> GetListQueryable();
        List<T> GetList(Expression<Func<T, bool>> _lamda);
        bool Any(Expression<Func<T, bool>> _lamda);
        void Dispose();
        T FindByExpression(Expression<Func<T, bool>> _lamda);
    }
}
