using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected LeagueDbContext ctx;

        public Repository(LeagueDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }
        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract void Update(T item);
        public abstract T Read(int id);
    }
}
