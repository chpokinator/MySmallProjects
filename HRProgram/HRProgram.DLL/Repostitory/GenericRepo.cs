using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.DLL.Repostitory
{
    public abstract class GenericRepo<T> : IRepo<T> where T : class
    {
        DbContext context;
        DbSet<T> table;

        public GenericRepo(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public void CreateOrUpdate(T entity)
        {
            table.AddOrUpdate(entity);

            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public T Get(int id)
        {
            return table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return table;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
