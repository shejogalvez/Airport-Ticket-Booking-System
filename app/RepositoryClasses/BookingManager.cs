namespace app.RepositoryClasses;

using app.Model;
static class BookingsManager
{
    private class CustomDictionary : Dictionary<string, Dictionary<Guid, Booking>>, IEnumerable<Booking>
    {
        IEnumerator<Booking> IEnumerable<Booking>.GetEnumerator()
        {
            foreach (var userBookings in Values)
            {
                foreach (var booking in userBookings.Values)
                {
                    yield return booking;
                }
            }
        }
    }

    private readonly static CustomDictionary bookings = [];

    public static IEnumerable<Booking> Bookings => bookings;
    public static void AddBooking(IUser user, Flight flight)
    {
        Booking booking = new (user, flight);
        var id = booking.ID;
        bookings.TryAdd(user.Username, []);
        bookings[user.Username].Add(id, booking);
    }

    // returns true if key exists
    public static bool RemoveBookingByUserAndID(IUser user, Guid id)
    {
        if (!bookings.TryGetValue(user.Username, out Dictionary<Guid, Booking>? value)) return false;
        return value.Remove(id);
    }
    public static IEnumerable<Booking> GetBookingsByPassenger(IUser user)
    {
        bookings.TryGetValue(user.Username, out Dictionary<Guid, Booking>? value);
        return value is not null ? value.Select(key_value => key_value.Value) : [];
    }

    // returns true if key exists
    public static bool UpdateBookingByUserAndID(IUser user, Guid bookingID, Flight newFlight)
    {
        bookings.TryGetValue(user.Username, out Dictionary<Guid, Booking>? value);
        if (value is null) return false;
        var res = value.TryGetValue(bookingID, out Booking? booking);
        if (!res) return false;
        value[bookingID] = booking! with {Flight = newFlight};
        return true;
    }
    public static bool RemoveBookingByUserAndID(IUser user, string id) => 
        Guid.TryParse(id, out Guid guid) && RemoveBookingByUserAndID(user, guid);
    public static bool UpdateBookingByUserAndID(IUser user, string id, Flight newFlight)=> 
        Guid.TryParse(id, out Guid guid) && UpdateBookingByUserAndID(user, guid, newFlight);
}