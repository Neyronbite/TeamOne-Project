using Common.Tools;
using DAL.Interfaces;
using System.Configuration;

namespace DAL.DataBase
{
    public class BaseDB<T> : IDB<T> where T : new()
    {
        protected string root;
        protected string tablePath;
        protected string tableOptionsPath;

        protected Options options;

        static BaseDB()
        {
            try
            {
                CheckDB();
            }
            catch (Exception)
            {

            }
        }

        public BaseDB()
        {
            root = ConfigurationManager.AppSettings.Get("root")!;

            var name = typeof(T).Name;
            tablePath = $"{root}\\{name}\\{name}Table.csv";
            tableOptionsPath = $"{root}\\{name}\\{name}TableOpt.json";

            options = new Options(tableOptionsPath);
        }

        //TODO optimize
        public void Delete(int id)
        {
            options.Init();

            var sb = new List<string>();
            using (var sr = new StreamReader(tablePath))
            {
                for (int i = 0; i < options.Lines; i++)
                {
                    string line = sr.ReadLine()!;
                    var temp = line.DeSerializeCSV<T>();

                    if (!((temp.GetID() == id)))
                    {
                        sb.Add(line);
                    }
                }
            }

            using (var sw = new StreamWriter(tablePath))
            {
                for (int i = 0; i < sb.Count; i++)
                {
                    sw.WriteLine(sb[i]);
                }
            }

            options.Lines--;
        }

        public List<T> Select(Func<T, bool> predicate, int limit = int.MaxValue)
        {
            options.Init();
            var list = new List<T>();

            using (var sr = new StreamReader(tablePath))
            {
                for (int i = 0; i < options.Lines; i++)
                {
                    string line = sr.ReadLine()!;
                    var entity = line.DeSerializeCSV<T>();

                    if (predicate(entity))
                    {
                        list.Add(entity);

                        if (list.Count >= limit)
                        {
                            return list;
                        }
                    }
                }
            }

            return list;
        }

        public T Insert(T entity)
        {
            options.Init();
            entity.SetID(options.Index);

            using (var sw = new StreamWriter(tablePath, true))
            {
                sw.WriteLine(entity!.SerializeCSV());
            }

            options.Index++;
            options.Lines++;
            return entity;
        }

        public T Update(T entity)
        {
            options.Init();

            var sb = new List<string>();

            using (var sr = new StreamReader(tablePath))
            {
                for (int i = 0; i < options.Lines; i++)
                {
                    string line = sr.ReadLine()!;
                    var temp = line.DeSerializeCSV<T>();

                    if ((temp.GetID() == entity.GetID()))
                    {
                        sb.Add(entity!.SerializeCSV());
                    }
                    else
                    {
                        sb.Add(line);
                    }
                }
            }

            using var sw = new StreamWriter(tablePath);
            for (int i = 0; i < sb.Count; i++)
            {
                sw.WriteLine(sb[i]);
            }

            return entity;
        }

        private static void CheckDB()
        {
            var root = ConfigurationManager.AppSettings.Get("root");
            var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName + "\\DAL\\Entities";

            var files = Directory.GetFiles(path);
            var rootDirectories = Directory.GetDirectories(root!);

            var getName = delegate (string file, string path) { return file.Replace(path + "\\", "").Replace(".cs", ""); };

            foreach (var file in files)
            {
                var name = getName(file, path);
                var directory = root + "\\" + name;

                if (!rootDirectories.Contains(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var rDFiles = Directory.GetFiles(directory);

                if (!rDFiles.Contains(directory + "\\" + name + "Table.csv"))
                {
                    using FileStream fs = File.Create(directory + "\\" + name + "Table.csv");
                }

                var jsonName = directory + "\\" + name + "TableOpt.json";

                if (!rDFiles.Contains(directory + "\\" + name + "TableOpt.json"))
                {
                    using FileStream fs = File.Create(jsonName);
                }

                bool write = false;

                using (var sw = new StreamReader(jsonName))
                {
                    write = string.IsNullOrWhiteSpace(sw.ReadToEnd());
                }

                if (write)
                {
                    using var sw = new StreamWriter(jsonName);
                    sw.WriteLine("{\"index\":0,\"lines\":0}");
                }
            }
        }
    }
}
