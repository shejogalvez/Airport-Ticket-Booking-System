using System.ComponentModel.DataAnnotations;
using app.Attributes;

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
    DateTime? DepartureDate, 
    string DepartureAirport, 
    string ArrivalAirport, 
    FlightClass? Class
) : IFlightFilterable
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Price { get; init; } = Price;

    [Required]
    public string DepartureCountry { get; init; } = DepartureCountry;

    [Required]
    public string DestinationCountry { get; init; } = DestinationCountry;
    
    [Required]
    [NotInPast]
    public DateTime? DepartureDate { get; init; } = DepartureDate;
    
    [Required]
    [LengthEquals(3)]
    public string DepartureAirport { get; init; } = DepartureAirport;
    
    [Required]
    [LengthEquals(3)]
    public string ArrivalAirport { get; init; } = ArrivalAirport;
    
    public FlightClass? Class { get; init; } = Class;
    Flight IFlightFilterable.Flight => this;
};