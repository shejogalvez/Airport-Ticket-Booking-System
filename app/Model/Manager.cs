
using app.RepositoryClasses;
using app.Utils;
using app.Attributes;

namespace app.Model;

partial class Manager(string username) : IUser
{
    public string Username { get; init; } = username;
    private readonly string _password = "admin"; // hardcoded for simplicity

    public bool Authorize(string password) => password == _password;
    
    private readonly FlightQueryComponent<Booking> QueryComponent = new (BookingsManager.Bookings);

    public void ExecuteCommand(string input)
    {
        var (command, args, options)  = IOUtils.ParseCommand(input);
        switch (command)
        {
            case "a":
            case "all_bookings":
                QueryComponent.ResetQuery();
                QueryComponent.DisplayQueryResults();
                break;
            case "q":
            case "query":
                QueryComponent.ExecuteQuery(args);
                QueryComponent.DisplayQueryResults();
                break;
            case "i":
            case "import":
                if (args.Length < 1)
                {
                    Console.WriteLine("path to csv file is required");
                }
                // Console.WriteLine($"options: {string.Join(' ', options)}");
                bool validate = options.Contains("-v");
                bool skip = options.Contains("-s");
                FlightDataManager.ImportFromCsv(args[0], validate, skip);
                break;
            case "v":
            case "validation_details":
                var metadata = ValidationMetadataGenerator.Generate<Flight>();

                foreach (var field in metadata)
                {
                    Console.WriteLine($"- *{field.FieldName}:*");
                    Console.WriteLine($"    - Type: {field.TypeDescription}");
                    Console.WriteLine(field.Constraints.Count > 0 ? $"    - Constraint: {string.Join(", ", field.Constraints)}\n" : "");
                }
                break;
            default:
                Console.WriteLine("invalid command, to show commands type help");
                break;
        }
    }

    public void ShowCommands()
    {
        var instructions = 
        """
        help - show this help message
        all_bookings (a) - view all bookings made in the system
        import (i) [-v] [-s] <file_path> - import flight data from a CSV file
            -v,     validates data and show detailed list of errors
            -s,     skips invalid records during import
        validation_details (v) - show details of validation constraints for each field in the data model
        query (q) <Parameter> <Argument> - filters last displayed bookings to where Parameter == Argument
        query (q) <Parameter> <min> <max> - filters last displayed bookings for min <= Parameter <= max
        """;

        Console.WriteLine(instructions);
    }

}