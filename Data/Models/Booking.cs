namespace Data.Models;

public class Booking
{
    public string Id { get; set; } = default!;
    public string EventId { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string StreetName { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public int TicketQuantity { get; set; }
}
