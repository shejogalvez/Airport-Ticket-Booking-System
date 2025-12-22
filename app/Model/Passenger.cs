using System.Text.RegularExpressions;
using app.RepositoryClasses;
using app.Utils;

namespace app.Model;

public partial class Passenger(string username) : IUser
{
    public string Username { get; init; } = username;

    public bool Authorize(string password) => true; // for the moment passenger don't require authorization

    private readonly FlightQueryComponent<Flight> QueryComponent = new (FlightDataManager.GetAllFlights());

    public override string ToString() => Username;
    public void ExecuteCommand(string input)
    {
        var (command, args, _options) = IOUtils.ParseCommand(input);
        switch (command)
        {
            case "a":
            case "all_flights":
                QueryComponent.ResetQuery();
                QueryComponent.DisplayQueryResults();
                break;
            case "q":
            case "query":
                if (QueryComponent.ExecuteQuery(args))
                    QueryComponent.DisplayQueryResults();
                else Console.WriteLine("Query Failed");
                break;
            case "b":
            case "booking":
                if (args.Length == 0)
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
                if (args.Length > 1)
                {
                    Console.WriteLine("write only an index of the list");
                    break;
                }
                int index = int.Parse(args[0]);
                var flight = QueryComponent.GetQueryResults().ElementAt(index-1);
                BookingsManager.AddBooking(this, flight);
                Console.WriteLine($"booking flight {args[0]} was successful");
                break;
            case "d":
            case "delete_booking":
                if (args.Length != 1)
                {
                    Console.WriteLine("provide only the ID of the booking");
                    break;
                }
                if (BookingsManager.RemoveBookingByUserAndID(this, args[0]))
                    Console.WriteLine("Booking deleted Successfully");
                else Console.WriteLine("Couldn't delete the booking, check the spelling of the ID provided");
                break;
            case "u":
            case "update_booking":
                if (args.Length != 2)
                {
                    Console.WriteLine("provide only the ID of the booking followed by the flight index");
                    break;
                }
                index = int.Parse(args[1]);
                flight = QueryComponent.GetQueryResults().ElementAt(index-1);
                if (BookingsManager.UpdateBookingByUserAndID(this, args[0], flight))
                    Console.WriteLine("Booking updated Successfully");
                else Console.WriteLine("Couldn't update the booking, check the spelling of the ID provided");
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
        delete_booking (d) <booking_id> - deletes the booking with the given ID made by the user
        update_booking (u) <booking_id> <flight_index> - changes flight of booking with given ID to the index-th flight showed in last displayed results
        """;

        Console.WriteLine(instructions);
    }

    [GeneratedRegex("\"(.*?)\"|\\S+")]
    private static partial Regex MyRegex();
}