using Common.Interfaces;
using Common.Models;
using Common.Tools;
using Newtonsoft.Json;
using TeamOneProject.Interfaces;

namespace TeamOneProject.Classes
{
    public class CookieManager : ICookie<User>
    {
        public bool IsAuthorized => GetData() != null;

        protected string root;
        protected ILogger logger;
        protected User data;

        public CookieManager()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var baseRoot = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            root = baseRoot + "\\Cookie.txt";

            if (!File.Exists(root))
            {
                using FileStream fs = File.Create(root);
            }

            logger = new Logger(baseRoot + "\\CookieLog.txt");
            data = null;
        }

        public void ClearData()
        {
            using (StreamWriter sw = new StreamWriter(root))
            {
                sw.WriteLine("");
            }

            data = null;

            logger.Log("Data cleared");
        }

        public User GetData()
        {
            if (data != null)
            {
                return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(data));
            }

            using (StreamReader sr = new StreamReader(root))
            {
                try
                {
                    string encodedData = sr.ReadToEnd();
                    string json = Base64Decode(encodedData);
                    data = JsonConvert.DeserializeObject<User>(json);

                    if (data != null)
                    {
                        logger.Log("Data loaded from Cookie");
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(data));
        }

        public void WriteData(User cookie)
        {
            try
            {
                string json = JsonConvert.SerializeObject(cookie);
                string encodedData = Base64Encode(json);

                if (string.IsNullOrWhiteSpace(encodedData))
                {
                    throw new NullReferenceException();
                }

                using (StreamWriter sw = new StreamWriter(root))
                {
                    sw.WriteLine(encodedData);
                }
                data = cookie;

                logger.Log($"Data updated - new User id is {cookie.ID}");
            }
            catch (Exception e)
            {
                logger.LogException(Common.Enums.Operations.Update, e);
            }
        }

        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        protected string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
