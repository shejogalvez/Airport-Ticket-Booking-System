namespace app;

static class BookingsManager
{
    private readonly static Dictionary<Guid, Booking> bookings = [];

    public static void AddBooking(Passenger passenger, Flight flight)
    {
        Booking booking = new (passenger, flight);
        var id = booking.ID;
        bookings.Add(id, booking);
    }

    // returns true if key exists
    public static bool RemoveBookingByID(Guid id)
    {
        return bookings.Remove(id);
    }
    public static IEnumerable<Booking> GetBookingsByPassenger(Passenger passenger)
    {
        return bookings
        .Select(key_value => key_value.Value)
        .Where(booking => booking.Passenger == passenger);
    }

    // returns true if key exists
    public static bool UpdateBookingByID(Guid bookingID, Flight newFlight)
    {
        var res = bookings.TryGetValue(bookingID, out Booking? booking);
        if (!res) return false;
        bookings[bookingID] = booking! with {Flight = newFlight};
        return true;
    }
}