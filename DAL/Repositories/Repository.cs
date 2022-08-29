using Common.Enums;
using Common.Interfaces;
using Common.Tools;
using DAL.DataBase;
using DAL.Interfaces;
using System.Configuration;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        protected ILogger logger;
        protected IDB<T> baseDB;

        public Repository()
        {
            baseDB = new BaseDB<T>();

            var name = typeof(T).Name;
            string loggerPath = $"{ConfigurationManager.AppSettings.Get("root")}\\{name}\\{name}Logs.txt";

            logger = new Logger(loggerPath);
        }

        public virtual T Add(T item)
        {
            var res = baseDB.Insert(item);
            logger.Log(Operations.Create, res);
            return res;
        }

        public virtual void Delete(T item)
        {
            baseDB.Delete(item.GetID());
            logger.Log(Operations.Delete, item);
        }

        public virtual List<T> Get(Func<T, bool> query, int limit = int.MaxValue)
        {
            var result = baseDB.Select(query, limit);
            logger.Log(Operations.Read, result, true);
            return result;
        }

        public virtual List<T> GetAll()
        {
            logger.Log($"Operation - Get, object - all objects");
            return baseDB.Select(e => true);
        }

        public virtual T GetById(int id)
        {
            var result = baseDB.Select(e => e.GetID() == id, 1).FirstOrDefault()!;
            logger.Log(Operations.Read, result);
            return result;
        }

        public virtual T Update(T item)
        {
            var res = baseDB.Update(item);
            logger.Log(Operations.Update, res);
            return res;
        }
    }
}
