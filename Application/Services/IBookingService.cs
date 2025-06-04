using Application.Models;
using Data.Models;

namespace Application.Services
{
    public interface IBookingService
    {
        Task<BookingResult> CreateBookingAsync(CreateBookingRequest request);
        Task<BookingResult<Booking?>> GetBookingAsync(string id);
        Task<BookingResult<List<Booking>>> GetBookingsByEmailAsync(string email);
    }
}