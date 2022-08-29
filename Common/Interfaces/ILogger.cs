using Common.Enums;

namespace Common.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string message, bool dontPrintDateTime);
        void Log(Operations operation, object obj);
        void Log<T>(Operations operation, IEnumerable<T> obj, bool isIEnumerable);
        void Log(Operations operation, object obj, int userID, UserType userType);
        void LogException(Operations operation, Exception exception);
        void LogException(Operations operation, Exception exception, int userID, UserType userType);
    }
}
