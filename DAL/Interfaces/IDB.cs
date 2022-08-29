namespace DAL.Interfaces
{
    public interface IDB<T>
    {
        List<T> Select(Func<T, bool> query, int limit = int.MaxValue);
        T Update(T entity);
        void Delete(int id);
        T Insert(T entity);
    }
}
