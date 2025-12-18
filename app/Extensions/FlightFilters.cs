namespace app.Extensions;

using app.Model;
public static class FlightsHelperExtension
{
    // get filtered data methods
    public static IEnumerable<Flight> FilterByDepartureCountry(this IEnumerable<Flight> FlightsArray, string country)
    {
        return FlightsArray.Where(flight => flight.DepartureCountry == country);
    }

    public static IEnumerable<Flight> FilterByDestinationCountry(this IEnumerable<Flight> FlightsArray, string country)
    {
        return FlightsArray.Where(flight => flight.DestinationCountry == country);
    }

    public static IEnumerable<Flight> FilterByDate(this IEnumerable<Flight> FlightsArray, DateTime date)
    {
        return FlightsArray.Where(flight => flight.DepartureDate == date);
    }

    public static IEnumerable<Flight> FilterByDateRange(this IEnumerable<Flight> FlightsArray, DateTime startDate, DateTime endDate)
    {
        return FlightsArray.Where(flight => flight.DepartureDate >= startDate && flight.DepartureDate <= endDate);
    }

    public static IEnumerable<Flight> FilterByClass(this IEnumerable<Flight> FlightsArray, FlightClass flightClass)
    {
        return FlightsArray.Where(flight => flight.Class == flightClass);
    }
    
    public static IEnumerable<Flight> FilterByPriceRange(this IEnumerable<Flight> FlightsArray, int minPrice, int maxPrice)
    {
        return FlightsArray.Where(flight => flight.Price >= minPrice && flight.Price <= maxPrice);
    }
}