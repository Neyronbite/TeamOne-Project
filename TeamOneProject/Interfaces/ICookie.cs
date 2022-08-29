namespace TeamOneProject.Interfaces
{
    public interface ICookie<T>
    {
        bool IsAuthorized { get; }

        T GetData();

        void WriteData(T cookie);

        void ClearData();
    }
}
