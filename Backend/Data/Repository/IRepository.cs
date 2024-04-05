namespace Backend.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        T Read(string id);
        void Delete(string id);
    }
}
