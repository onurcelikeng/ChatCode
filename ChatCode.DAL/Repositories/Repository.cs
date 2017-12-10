using ChatCode.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChatCode.DAL.Repositories
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        DataContext context;
        DbSet<T> Table;


        public Repository()
        {
            context = new DataContext();
            Table = context.Set<T>();
        }

        ~Repository()
        {
            Dispose();
        }


        public DbSet<T> Context()
        {
            return Table;
        }

        public bool Add(T obj)
        {
            Table.Add(obj);
            return Save();
        }

        public bool Delete(T obj)
        {
            Table.Remove(obj);
            return Save();
        }

        public T Find(object id)
        {
            return Table.Find(id);
        }

        public List<T> List()
        {
            return Table.ToList();
        }

        public bool Update(T obj)
        {
            return Save();
        }

        public List<T> Where(Func<T, bool> where)
        {
            return Table.Where(where).ToList();
        }

        public T First(Func<T, bool> where)
        {
            return Table.FirstOrDefault(where);
        }

        private bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //LogHelper.Log("Veritabanında işlem hatası: " + ex.Message, LogType.Error, ex);
                return false;
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}