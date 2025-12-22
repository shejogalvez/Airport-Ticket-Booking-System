namespace app.RepositoryClasses;

using app.Model;

public class BookingQueryComponent(IEnumerable<Booking> allData) : FlightQueryComponent<Booking>(allData)
{
    public override bool ExecuteQuery(string[]? args = null)
    {
        if (args is null) return false;
        if (args.Length == 2 && args[0] == "Passenger")
        {
            QueryResult = QueryResult.Where(b => ((Booking) b).Passenger.Username == args[1]);
            return true;
        }
        else return base.ExecuteQuery(args);
        
    }
}