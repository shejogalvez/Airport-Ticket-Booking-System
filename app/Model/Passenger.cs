using System.Text.RegularExpressions;
using app.RepositoryClasses;

namespace app.Model;

using Parser = app.Utils.IOParser;
public partial class Passenger(string username) : IUser
{
    public string Username { get; init; } = username;

    public bool Authorize(string password) => true; // for the moment passenger don't require authorization


    

    private IEnumerable<Flight> QueryResult { get => field ?? FlightDataManager.GetAllFlights();  set; }

    private void DisplayFlightList()
    {
        for (int i=0; i<QueryResult.Count(); i++)
        {
            var item = QueryResult.ElementAt(i);
            Console.WriteLine($"{i+1}. {item}");
        }
    }
    public void ExecuteCommand(string command)
    {
        var matches = MyRegex().Matches(command).Select(m => m.ToString());
        switch (matches.First())
        {
            case "a":
            case "all_flights":
                QueryResult = FlightDataManager.GetAllFlights();
                DisplayFlightList();
                break;
            case "q":
            case "query":
                if (matches.Count() < 3)
                {
                    Console.WriteLine("missing arguments for querying");
                    break;
                }
                if (matches.Count() > 4)
                {
                    Console.WriteLine("too many arguments arguments for querying");
                    break;
                }
                if (matches.Count() == 3)
                    QueryResult = Parser.ParseQuery(QueryResult, matches.ElementAt(1), matches.ElementAt(2));
                
                else if (matches.Count() == 4)
                    QueryResult = Parser.ParseQuery(QueryResult, matches.ElementAt(1), matches.ElementAt(2), matches.ElementAt(3));

                DisplayFlightList();
                break;
            case "b":
            case "booking":
                if (matches.Count() == 1)
                {
                    var bookings = BookingsManager.GetBookingsByPassenger(this);
                    if (!bookings.Any()) Console.WriteLine($"No bookings made by user {Username}");
                    else
                    {
                        Console.WriteLine($"bookings made by {Username}:");
                        foreach (var booking in bookings)
                            Console.WriteLine($" - {booking}");
                    }
                    break;
                }
                if (matches.Count() != 2)
                {
                    Console.WriteLine("write only an index of the list");
                    break;
                }
                int index = int.Parse(matches.ElementAt(1));
                var flight = QueryResult.ElementAt(index-1);
                BookingsManager.AddBooking(this, flight);
                Console.WriteLine($"booking flight {matches.ElementAt(1)} was successful");
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
        all_flights (a) - view all available flights
        query (q) <Parameter> <Argument> - filters last displayed results to where Parameter == Argument
        query (q) <Parameter> <min> <max> - filters last displayed results for min <= Parameter <= max
        booking (b) - display bookings made by yourself
        booking (b) <(int) index> - book the index-th flight showed in last displayed results
        """;

        Console.WriteLine(instructions);
    }

    [GeneratedRegex("\"(.*?)\"|\\S+")]
    private static partial Regex MyRegex();
}