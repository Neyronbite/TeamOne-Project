using BLL.Setup;
using TeamOneProject.Classes;

namespace TeamOneProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Startup.Run();
            var pi = new ProgramInterface();
            try
            {
                pi.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                pi.Exit();
            }
        }
    }
}