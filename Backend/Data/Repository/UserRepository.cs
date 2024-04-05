using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override User Read(string id)
        {
            return ctx.Users.FirstOrDefault(x => x.Id == id);
        }

        public override User Read(int id)
        {
            throw new NotImplementedException();
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
