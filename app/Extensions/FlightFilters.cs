namespace app.Extensions;

using app.Model;
public static class FlightsHelperExtension
{
    // get filtered data methods
    public static IEnumerable<IFlightFilterable> FilterByDepartureCountry(this IEnumerable<IFlightFilterable> FlightsArray, string country)
    {
        return FlightsArray.Where(obj => obj.Flight.DepartureCountry == country);
    }

    public static IEnumerable<IFlightFilterable> FilterByDestinationCountry(this IEnumerable<IFlightFilterable> FlightsArray, string country)
    {
        return FlightsArray.Where(obj => obj.Flight.DestinationCountry == country);
    }

    public static IEnumerable<IFlightFilterable> FilterByDate(this IEnumerable<IFlightFilterable> FlightsArray, DateTime date)
    {
        return FlightsArray.Where(obj => obj.Flight.DepartureDate == date);
    }

    public static IEnumerable<IFlightFilterable> FilterByDateRange(this IEnumerable<IFlightFilterable> FlightsArray, DateTime startDate, DateTime endDate)
    {
        return FlightsArray.Where(obj => obj.Flight.DepartureDate >= startDate && obj.Flight.DepartureDate <= endDate);
    }

    public static IEnumerable<IFlightFilterable> FilterByClass(this IEnumerable<IFlightFilterable> FlightsArray, FlightClass flightClass)
    {
        return FlightsArray.Where(obj => obj.Flight.Class == flightClass);
    }
    
    public static IEnumerable<IFlightFilterable> FilterByPriceRange(this IEnumerable<IFlightFilterable> FlightsArray, int minPrice, int maxPrice)
    {
        return FlightsArray.Where(obj => obj.Flight.Price >= minPrice && obj.Flight.Price <= maxPrice);
    }

    public static IEnumerable<IFlightFilterable> FilterByDepartureAirport(this IEnumerable<IFlightFilterable> FlightsArray, string airport)
    {
        return FlightsArray.Where(obj => obj.Flight.DepartureAirport == airport);
    }
    public static IEnumerable<IFlightFilterable> FilterByArrivalAirport(this IEnumerable<IFlightFilterable> FlightsArray, string airport)
    {
        return FlightsArray.Where(obj => obj.Flight.ArrivalAirport == airport);
    }
}