using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public interface IGenericDataRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T GetByID(object id);
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(object id);
        bool Delete(T obj);

        void Save();
    }
}