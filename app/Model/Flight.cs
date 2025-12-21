namespace app.Model;

[Flags]
public enum ErrorCode
{
    NoErrors                = 0b_0000_0000,
    NoPrice                 = 0b_0000_0001,
    NegativePrice           = 0b_0000_0010,
    NoDepartureCountry      = 0b_0000_0100,
    NoDestinationCountry    = 0b_0000_1000,
    NoDepartureAirport      = 0b_0001_0000,
    NoArrivalAirport        = 0b_0010_0000,
    PastDepartureDate       = 0b_0100_0000,
}

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
)
) : IFlightFilterable
{
    // return a string with the error 
    public ErrorCode GetErrors()
    {
        var error = ErrorCode.NoErrors;
        if (Price < 0) error |= ErrorCode.NegativePrice;
        if (string.IsNullOrWhiteSpace(DepartureCountry)) error |= ErrorCode.NoDepartureCountry;
        if (string.IsNullOrWhiteSpace(DestinationCountry)) error |= ErrorCode.NoDestinationCountry;
        if (string.IsNullOrWhiteSpace(DepartureAirport)) error |= ErrorCode.NoDepartureAirport;
        if (string.IsNullOrWhiteSpace(ArrivalAirport)) error |= ErrorCode.NoArrivalAirport;
        if (DepartureDate < DateTime.Now) error |= ErrorCode.PastDepartureDate;
        return error;
    }
};