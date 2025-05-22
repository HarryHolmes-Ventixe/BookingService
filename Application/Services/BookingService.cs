using Application.Models;
using Data.Entities;
using Data.Repositories;

namespace Application.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingResult> CreateBookingAsync(CreateBookingRequest request)
    {
        try
        {
            var bookingEntity = new BookingEntity
            {
                EventId = request.EventId,
                TicketQuantity = request.TicketQuantity,
                BookingDate = DateTime.Now,
                BookingOwner = new BookingOwnerEntity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Address = new BookingAddressEntity
                    {
                        StreetName = request.StreetName,
                        PostalCode = request.PostalCode,
                        City = request.City
                    }
                }
            };

            var result = await _bookingRepository.AddAsync(bookingEntity);
            return result.Success
                ? new BookingResult
                {
                    Success = true
                }
                : new BookingResult
                {
                    Success = false,
                    Error = "Failed to create booking."
                };
        }
        catch (Exception ex)
        {
            return new BookingResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }
}
