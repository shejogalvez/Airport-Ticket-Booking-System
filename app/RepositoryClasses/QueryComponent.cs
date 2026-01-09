namespace app.RepositoryClasses;

using app.Extensions;
using app.Model;
public class FlightQueryComponent<T>(IEnumerable<T> allData) where T : IFlightFilterable
{
    private IEnumerable<IFlightFilterable> AllData { get; } = (IEnumerable<IFlightFilterable>) allData;
    protected IEnumerable<IFlightFilterable> QueryResult { get; set; } = (IEnumerable<IFlightFilterable>) allData;

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
        int i = 0;
        foreach (var item in QueryResult)
        {
            Console.WriteLine($"{++i}. {item}");
        }
    }

    // executes query from args and returns true if query was successful
    public virtual bool ExecuteQuery(string[]? args = null)
    {
        // return all results
        if (args is null) return false;
        
        
        if (args.Length < 2)
        {
            Console.WriteLine("missing arguments for querying");
            return false;
        }
        if (args.Length > 3)
        {
            Console.WriteLine("too many arguments arguments for querying");
            return false;
        }
        if (args.Length == 2)
        {
            var q = ParseQuery(QueryResult, args[0], args[1]);
            if (q is null) return false;
            QueryResult = q;
        }
        else if (args.Length == 3)
        {
            var q = ParseQuery(QueryResult, args[0], args[1], args[2]);
            if (q is null) return false;
            QueryResult = q;
        }
        return true;
    }

    private static IEnumerable<IFlightFilterable>? ParseQuery(IEnumerable<IFlightFilterable> flights, string parameter, string argument)
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
                Console.WriteLine($"undefined property {parameter} of Flight class");
                return null;
        }
    }
    private static IEnumerable<IFlightFilterable>? ParseQuery(IEnumerable<IFlightFilterable> flights, string parameter, string min, string max)
    {
        switch (parameter) {
            case "Price":
                int mini = int.Parse(min);
                int maxi = int.Parse(max);
                return flights.FilterByPriceRange(mini, maxi);
            case "DepartureDate": 
                return flights.FilterByDateRange(DateTime.Parse(min), DateTime.Parse(max));
            default: 
                Console.WriteLine($"undefined property {parameter} of Flight class");
                return null;
        }
    }
}