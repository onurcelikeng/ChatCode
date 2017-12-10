using System;
using System.Collections.Generic;

namespace ChatCode.DAL.Repositories
{
    interface IRepository<T> where T : class
    {
        List<T> List();

        T Find(object id);

        List<T> Where(Func<T, bool> where);

        bool Add(T obj);

        bool Update(T obj);

        bool Delete(T obj);
    }
}