using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(LeagueDbContext context) : base(context)
        {
        }
        public override Player Read(int id)
        {
            return ctx.Players.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Player item)
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
