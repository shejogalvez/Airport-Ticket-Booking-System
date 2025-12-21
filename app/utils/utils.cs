namespace app.Utils;

public static class IOUtils
{
    public static string? ReadInput()
    {
        Console.Write("> ");
        return Console.ReadLine();
    }

}