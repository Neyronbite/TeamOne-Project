using Common.Enums;
using Common.Interfaces;

namespace Common.Tools
{
    public class Logger : ILogger
    {
        //TODO
        protected string filePath;

        public Logger(string filePath)
        {
            this.filePath = filePath;

            if (!File.Exists(filePath))
            {
                using FileStream fs = File.Create(filePath);
            }
        }

        public void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{message}: {DateTime.Now}");
            }
        }

        public void Log(string message, bool dontPrintDateTime)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }
        }

        public void Log(Operations operation, object obj)
        {
            string objInfo = obj.TryGetID(out int id) ? id.ToString() : obj.GetType().Name;

            string message = $"Operation - {operation}, object - {objInfo}: {DateTime.Now}";

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }
        }

        public void Log<T>(Operations operation, IEnumerable<T> enumerable, bool isIEnumerable)
        {
            foreach (var item in enumerable)
            {
                Log(operation, item);
            }
        }

        public void Log(Operations operation, object obj, int userID, UserType userType)
        {
            string objInfo = obj.TryGetID(out int id) ? id.ToString() : obj.GetType().Name;

            string message = $"Operation - {operation}, object - {objInfo}, done by - {userType} id - {userID}: {DateTime.Now}";

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }
        }

        public void LogException(Operations operation, Exception exception)
        {
            string exceptionInfo = exception.Message;

            string message = $"Operation - {operation}, object - {exceptionInfo}: {DateTime.Now}";

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }
        }

        public void LogException(Operations operation, Exception exception, int userID, UserType userType)
        {
            string exceptionInfo = exception.Message;

            string message = $"Operation - {operation}, exception ocured - {exceptionInfo}, done by - {userType} id - {userID}: {DateTime.Now}";

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }
        }
    }
}
