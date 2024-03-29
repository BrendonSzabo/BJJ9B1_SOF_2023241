using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(LeagueDbContext context) : base(context)
        {
        }
        public override User Read(int id)
        {
            return ctx.Users.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(User item)
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
