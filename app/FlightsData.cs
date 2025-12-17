enum FlightClass
{
    Economy, 
    Business, 
    FirstClass
}

static class FlightDataManager
{
    // TODO: consider using custom types instead of strings for countries and airports
    public record Flight(
        int Price, 
        string DepartureCountry, 
        string DestinationCountry, 
        DateTime DepartureDate, 
        string DepartureAirport, 
        string ArrivalAirport, 
        FlightClass Class
    );


    static Flight[]? _data;

    public static Flight[] Data
    {
        get {
            if (_data is null) {
                // TODO: populate with data from "database"
                Flight[] test_data = [
                    new Flight(100, "USA", "Canada", new DateTime(2025, 12, 23), "JFK", "YYZ", FlightClass.Economy),
                    new Flight(200, "USA", "Canada", new DateTime(2025, 12, 24), "LAX", "YYZ", FlightClass.Business),
                    new Flight(300, "USA", "Canada", new DateTime(2025, 12, 25), "JFK", "YYZ", FlightClass.FirstClass),
                    new Flight(50, "Chile", "Argentina", new DateTime(2025, 12, 26), "SCL", "REL", FlightClass.Economy),
                    new Flight(80, "Chile", "Argentina", new DateTime(2025, 12, 28), "SCL", "CSZ", FlightClass.Business),
                    new Flight(100, "Chile", "Argentina", new DateTime(2025, 12, 28), "SCL", "REL", FlightClass.FirstClass),
                    new Flight(200, "CountryA", "CountryB", new DateTime(2025, 12, 29), "ABC", "DFG", FlightClass.Economy),
                    new Flight(350, "CountryB", "CountryA", new DateTime(2025, 12, 29), "DFG", "ABC", FlightClass.FirstClass),
                ];
                _data = test_data;
            }
            return _data;
        }
    }

    public static IEnumerable<Flight> GetAllFlights()
    {
        return Data;
    }

    // get filtered data methods
    public static IEnumerable<Flight> FilterByDepartureCountry(IEnumerable<Flight> FlightsArray, string country)
    {
        return FlightsArray.Where(flight => flight.DepartureCountry == country);
    }

    public static IEnumerable<Flight> FilterByDestinationCountry(IEnumerable<Flight> FlightsArray, string country)
    {
        return FlightsArray.Where(flight => flight.DestinationCountry == country);
    }

    public static IEnumerable<Flight> FilterByDate(IEnumerable<Flight> FlightsArray, DateTime date)
    {
        return FlightsArray.Where(flight => flight.DepartureDate == date);
    }

    public static IEnumerable<Flight> FilterByDateRange(IEnumerable<Flight> FlightsArray, DateTime startDate, DateTime endDate)
    {
        return FlightsArray.Where(flight => flight.DepartureDate >= startDate && flight.DepartureDate <= endDate);
    }

    public static IEnumerable<Flight> FilterByClass(IEnumerable<Flight> FlightsArray, FlightClass flightClass)
    {
        return FlightsArray.Where(flight => flight.Class == flightClass);
    }
    
    public static IEnumerable<Flight> FilterByPriceRange(IEnumerable<Flight> FlightsArray, int minPrice, int maxPrice)
    {
        return FlightsArray.Where(flight => flight.Price >= minPrice && flight.Price <= maxPrice);
    }

}