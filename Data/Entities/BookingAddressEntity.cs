namespace Data.Entities;

public class BookingAddressEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    
}