using Microsoft.EntityFrameworkCore;
using NGA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGA.Data.SubStructure
{
    public interface IRepository<T>
    {
        Task<T> GetByID(Guid id);
        IQueryable<T> Get();
        IQueryable<T> Query();
        void Add(T entity);
        void Update(T entity);
    }

    public class Repository<T> : IRepository<T>
          where T : Base
    {
        private NGADbContext con;
        public Repository(NGADbContext context)
        {
            con = context;
        }
        public NGADbContext Context
        {
            get { return con; }
            set { con = value; }

        }
        public virtual async Task<T> GetByID(Guid id)
        {
            return await con.Set<T>().FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }
        public IQueryable<T> Get()
        {
            return con.Set<T>().AsQueryable();
        }
        public virtual void Add(T entity)
        {
            con.Set<T>().Add(entity);
        }
        public virtual void Update(T entity)
        {
            con.Entry(entity).State = EntityState.Modified;
        }
        public IQueryable<T> Query()
        {
            return con.Set<T>().AsNoTracking().Where(x => !x.IsDeleted);
        }
    }
}
