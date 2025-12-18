namespace app.Extensions;

using app.Model;

public readonly struct FlightFunctions
{
    public delegate bool EqualFilter<T>(Flight flight, T value);
    public delegate bool RangeFilter<T>(Flight flight, T value1, T value2);
    public readonly static EqualFilter<string> ByDepartureCountry = (flight, country) => 
        flight.DepartureCountry == country;

    public readonly static EqualFilter<string> ByDestinationCountry = (flight, country) =>
        flight.DestinationCountry == country ;

    public readonly static EqualFilter<DateTime> ByDate = (flight, date) =>
        flight.DepartureDate == date ;

    public readonly static RangeFilter<DateTime> ByDateRange = (flight, startDate, endDate) =>
        flight.DepartureDate >= startDate && flight.DepartureDate <= endDate;

    public readonly static EqualFilter<FlightClass> ByClass = (flight, flightClass) =>
        flight.Class == flightClass;

    public readonly static RangeFilter<int> ByPriceRange = (flight, minPrice, maxPrice) =>
        flight.Price >= minPrice && flight.Price <= maxPrice;
} 

public static class FlightsHelperExtension
{
    // get filtered data methods
    
    public static IEnumerable<Flight> FilterByRange<T>(this IEnumerable<Flight> flightsArray, FlightFunctions.RangeFilter<T> predicate, T value1, T value2)
    {
        return flightsArray.Where(flight => predicate(flight, value1, value2));
    }

    public static IEnumerable<Flight> FilterByEquals<T>(this IEnumerable<Flight> flightsArray, FlightFunctions.EqualFilter<T> predicate, T value)
    {
        return flightsArray.Where(flight => predicate(flight, value));
    }
}