namespace BLL.Interfaces
{
    public interface IProxyRepository<TEntity, TModel>
    {
        TModel GetById(int id);
        List<TModel> GetAll();
        List<TModel> Get(Func<TModel, bool> query, int limit = int.MaxValue);
        TModel Add(TModel item);
        TModel Update(TModel item);
        void Delete(TModel item);
    }
}
