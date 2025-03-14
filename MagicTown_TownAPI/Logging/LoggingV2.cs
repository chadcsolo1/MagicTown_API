namespace MagicTown_TownAPI.Logging
{
    public class LoggingV2
    {
        public void Log(string message, string type)
        {
            switch (type)
            {
                case "error":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR - " +  message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "warning":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("WARNING - " +  message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "info":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("WARNING - " +  message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
