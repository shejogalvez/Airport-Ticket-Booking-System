namespace app.Utils;

using System.Text.RegularExpressions;

public static partial class IOUtils
{
    public static string? ReadInput()
    {
        Console.Write("> ");
        return Console.ReadLine();
    }

    public static Tuple<string, string[], string[]> ParseCommand(string input)
    {
        var matches = CommandRegex().Matches(input);
        var optionMatches = matches.Where(m => m.Groups[2].Success).Select(m => m.Value);
        var command = matches.First().Value;
        HashSet<string> options = [.. optionMatches];
        var args = matches.Select(m => m.Value).Except(options).Except([command]);
        return new Tuple<string, string[], string[]> (command, [.. args], [.. options]);
    }


    [GeneratedRegex("\"(.*?)\"|(-.)|\\S+")]
    private static partial Regex CommandRegex();

}