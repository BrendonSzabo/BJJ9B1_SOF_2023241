using Backend.Models;

namespace Backend.Data.Repository
{
    public class MatchRepository : Repository<Match>, IRepository<Match>
    {
        public MatchRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override Match Read(int id)
        {
            return ctx.Matches.FirstOrDefault(t => t.Id == id);
        }

        public override Match Read(string id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Match item)
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
