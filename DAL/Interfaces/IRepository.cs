namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);

        List<T> GetAll();

        List<T> Get(Func<T, bool> query, int limit = int.MaxValue);

        T Add(T item);

        T Update(T item);

        void Delete(T item);
    }
}
