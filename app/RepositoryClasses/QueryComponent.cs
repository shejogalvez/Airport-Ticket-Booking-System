namespace app.RepositoryClasses;

using app.Extensions;
using app.Model;
public class FlightQueryComponent<T>(IEnumerable<T> allData) where T : IFlightFilterable
{
    private IEnumerable<IFlightFilterable> AllData { get; } = (IEnumerable<IFlightFilterable>) allData;
    private IEnumerable<IFlightFilterable> QueryResult { get; set; } = (IEnumerable<IFlightFilterable>) allData;

    public IEnumerable<T> GetQueryResults()
    {
        return QueryResult.Cast<T>();
    }

    public void ResetQuery()
    {
        QueryResult = AllData;
    }
    public void DisplayQueryResults()
    {
        for (int i=0; i<QueryResult.Count(); i++)
        {
            var item = QueryResult.ElementAt(i);
            Console.WriteLine($"{i+1}. {item}");
        }
    }

    // executes query from args and 
    public void ExecuteQuery(string[]? args = null)
    {
        // return all results
        if (args is null)
        {
            return;
        }
        // else
        if (args.Length < 2)
        {
            Console.WriteLine("missing arguments for querying");
            return;
        }
        if (args.Length > 3)
        {
            Console.WriteLine("too many arguments arguments for querying");
            return;
        }
        Console.WriteLine(string.Join(" ", args));
        if (args.Length == 2)
            QueryResult = ParseQuery(QueryResult, args[0], args[1]);
        
        else if (args.Length == 3)
            QueryResult = ParseQuery(QueryResult, args[0], args[1], args[2]);
    }

    private static IEnumerable<IFlightFilterable> ParseQuery(IEnumerable<IFlightFilterable> flights, string parameter, string argument)
    {
        switch (parameter) {
            case "Price":
                int price = int.Parse(argument);
                return flights.Where(f => f.Flight.Price == price); // query defined in-place
            case "DepartureCountry": 
                return flights.FilterByDepartureCountry(argument);
            case "DestinationCountry": 
                return flights.FilterByDestinationCountry(argument);
            case "DepartureAirport": 
                return flights.FilterByDepartureAirport(argument);
            case "ArrivalAirport": 
                return flights.FilterByArrivalAirport(argument);
            case "DepartureDate": 
                return flights.FilterByDate(DateTime.Parse(argument));
            case "Class": 
                FlightClass classParsed = Enum.Parse<FlightClass>(argument, true);
                return flights.FilterByClass(classParsed);
            default: 
                Console.WriteLine("undefined property of Flight class");
                return flights;
        }
    }
    private static IEnumerable<IFlightFilterable> ParseQuery(IEnumerable<IFlightFilterable> flights, string parameter, string min, string max)
    {
        switch (parameter) {
            case "Price":
                int mini = int.Parse(min);
                int maxi = int.Parse(max);
                return flights.FilterByPriceRange(mini, maxi);
            case "DepartureDate": 
                return flights.FilterByDateRange(DateTime.Parse(min), DateTime.Parse(max));
            default: 
                Console.WriteLine("undefined property of Flight class");
                return flights;
        }
    }

    // Overload with casting methods
    public static IEnumerable<Flight> ParseQuery(IEnumerable<Flight> flights, string parameter, string min, string max) 
        => ParseQuery(flights, parameter, min, max).Cast<Flight>();
        
    public static IEnumerable<Flight> ParseQuery(IEnumerable<Flight> flights, string parameter, string argument)
        => ParseQuery(flights, parameter, argument).Cast<Flight>();

    public static IEnumerable<Booking> ParseQuery(IEnumerable<Booking> flights, string parameter, string min, string max) 
        => ParseQuery(flights, parameter, min, max).Cast<Booking>();
        
    public static IEnumerable<Booking> ParseQuery(IEnumerable<Booking> flights, string parameter, string argument)
        => ParseQuery(flights, parameter, argument).Cast<Booking>();
    
}