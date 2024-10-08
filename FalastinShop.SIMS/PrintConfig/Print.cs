namespace FalastinShop.SIMS.PrintConfig;
public static class Print
{
    public static void ConfigSuccessConsole(string msg)
    {
        ConfigConsoleColor(msg, ConsoleColor.Green);
    }

    public static void ConfigErrorConsole(string msg)
    {
        ConfigConsoleColor(msg, ConsoleColor.Red);
    }

    private static void Log(string msg)
    {
        Console.WriteLine(msg);
    }

    private static void ConfigConsoleColor(string msg, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Log(msg);
        Console.ResetColor();
    }
}
