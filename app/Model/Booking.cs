namespace app.Model;

public record Booking(
    Passenger Passenger,
    Flight Flight
)
{
    public Guid ID {get;} = Guid.NewGuid();
    public DateTime CreatedAt {get; init;} = DateTime.Now;
    public DateTime ModifiedAt {get; set;} = DateTime.Now;
}