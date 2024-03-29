using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(LeagueDbContext context) : base(context)
        {
        }
        public override Team Read(int id)
        {
            return ctx.Matches.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Team item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
