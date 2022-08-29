using BLL.Interfaces;
using Common.Tools;
using DAL.Interfaces;

namespace BLL.Proxy
{
    public class ProxyRepository<TEntity, TModel> : IProxyRepository<TEntity, TModel>
        where TEntity : new()
        where TModel : new()
    {
        public int MaxCapacity { get; set; }
        private IRepository<TEntity> repository;
        private List<TModel> models;
        private bool isUpdated;

        public ProxyRepository(IRepository<TEntity> repository, int maxCapacity = 128)
        {
            this.models = new List<TModel>();
            this.repository = repository;
            this.MaxCapacity = maxCapacity;

            CheckRepo();
        }

        public TModel Add(TModel item)
        {
            var res = repository.Add(item.MapTo<TEntity>());

            if (isUpdated)
            {
                CheckRepo();
            }

            return res.MapTo<TModel>();
        }

        public void Delete(TModel item)
        {
            repository.Delete(item.MapTo<TEntity>());

            if (isUpdated)
            {
                models.Remove(GetById(item.GetID()));
            }
        }

        public List<TModel> Get(Func<TModel, bool> query, int limit = int.MaxValue)
        {
            return isUpdated ?
                models.Where(query).Take(limit).ToList() :
                repository.Get(query.PredicateConverter<TModel, TEntity>(), limit).MapTo<TModel, TEntity>();
        }

        public List<TModel> GetAll()
        {
            return isUpdated ? models : repository.GetAll().MapTo<TModel, TEntity>();
        }

        public TModel GetById(int id)
        {
            var res = models.Where(m => m.GetID() == id).FirstOrDefault();
            return res != null ? res : repository.GetById(id).MapTo<TModel>();
        }

        public TModel Update(TModel item)
        {
            var res = repository.Update(item.MapTo<TEntity>());

            if (isUpdated)
            {
                models.Remove(GetById(item.GetID()));
                models.Add(item);
            }

            return res.MapTo<TModel>();
        }

        private void CheckRepo()
        {
            List<TEntity> res = repository.Get(e => true, MaxCapacity);

            if (res?.Count < MaxCapacity)
            {
                models = res.MapTo<TModel, TEntity>();
                isUpdated = true;
            }
            else
            {
                models.Clear();
                isUpdated = false;
            }
        }
    }
}
