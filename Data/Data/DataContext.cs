using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<BookingAddressEntity> BookingAddresses { get; set; } = null!;
    public DbSet<BookingEntity> Bookings { get; set; } = null!;
    public DbSet<BookingOwnerEntity> BookingOwners { get; set; } = null!;
}

