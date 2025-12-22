namespace app.Model;

public record Booking(
    IUser Passenger,
    Flight Flight
) : IFlightFilterable
{
    public Guid ID {get;} = Guid.NewGuid();
    public DateTime CreatedAt {get; init;} = DateTime.Now;
    public DateTime ModifiedAt {get; set;} = DateTime.Now;

    public override string ToString() => 
        $"[ID: {ID}]    (Booked by: {Passenger})\n{Flight}";
}