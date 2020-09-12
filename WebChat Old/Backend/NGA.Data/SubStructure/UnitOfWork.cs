using NGA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGA.Data.SubStructure
{
    public class UnitOfWork : IDisposable
    {
        private NGADbContext con;
        private bool disposed = false;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(NGADbContext _con)
        {
            con = _con;
        }

        public IRepository<T> Repository<T>() where T : Base
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repository = new Repository<T>(con);
            repositories.Add(typeof(T), repository);
            return repository;
        }
        public virtual async Task<int> SaveChanges()
        {
            return await con.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    con.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>();
        Task<int> SaveChanges();
        void Dispose(bool disposing);
        void Dispose();
    }
}
