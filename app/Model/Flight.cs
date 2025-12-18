namespace app.Model;

public enum FlightClass
{
    Economy, 
    Business, 
    FirstClass
}

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