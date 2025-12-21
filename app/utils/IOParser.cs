namespace app.Utils;

using app.Model;
using app.Extensions;

public static class IOParser {
    
    public static IEnumerable<IFlightFilterable> ParseQuery(IEnumerable<IFlightFilterable> flights, string parameter, string argument)
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
    public static IEnumerable<IFlightFilterable> ParseQuery(IEnumerable<IFlightFilterable> flights, string parameter, string min, string max)
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