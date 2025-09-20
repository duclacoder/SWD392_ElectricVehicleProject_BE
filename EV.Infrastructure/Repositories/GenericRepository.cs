using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EV.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected Swd392Se1834G2T1Context _context;

        public GenericRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }
    }
}
