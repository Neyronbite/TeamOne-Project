namespace TeamOneProject.Classes.UI
{
    public class LoadingUI
    {
        public void PrintLoadingAnimation(CancellationToken token, string message = "Loading")
        {
            Task task = new Task(() =>
            {
                Console.SetCursorPosition(0, 2);
                Console.WriteLine(message);
                while (!token.IsCancellationRequested)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!token.IsCancellationRequested)
                        {
                            Console.SetCursorPosition(message.Length + i, 2);
                            Console.Write(".");
                        }
                        else
                        {
                            return;
                        }
                        Thread.Sleep(100);
                    }
                    if (!token.IsCancellationRequested)
                    {
                        Console.SetCursorPosition(message.Length, 2);
                        Console.Write("   ");
                    }
                    else
                    {
                        return;
                    }
                }
            }, token);
            task.Start();
        }
    }
}
