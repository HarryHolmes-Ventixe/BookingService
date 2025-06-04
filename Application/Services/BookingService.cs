using Application.Models;
using Data.Entities;
using Data.Models;
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
            if (result.Success)
            {
                var booking = new Booking
                {
                    Id = bookingEntity.Id,
                    EventId = bookingEntity.EventId,
                    TicketQuantity = bookingEntity.TicketQuantity,
                    FirstName = bookingEntity.BookingOwner?.FirstName ?? string.Empty,
                    LastName = bookingEntity.BookingOwner?.LastName ?? string.Empty,
                    Email = bookingEntity.BookingOwner?.Email ?? string.Empty,
                    StreetName = bookingEntity.BookingOwner?.Address?.StreetName ?? string.Empty,
                    PostalCode = bookingEntity.BookingOwner?.Address?.PostalCode ?? string.Empty,
                    City = bookingEntity.BookingOwner?.Address?.City ?? string.Empty
                };

                return new BookingResult
                {
                    Success = true,
                    Booking = booking
                };
            }
            else
            {
                return new BookingResult
                {
                    Success = false,
                    Error = "An error occurred while creating the booking."
                };
            }
                
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

    public async Task<BookingResult<Booking?>> GetBookingAsync(string id)
    {
        var result = await _bookingRepository.GetAsync(x => x.Id == id);
        if (result.Success && result.Result != null)
        {
            var booking = new Booking
            {
                Id = result.Result.Id,
                EventId = result.Result.EventId,
                TicketQuantity = result.Result.TicketQuantity,
                FirstName = result.Result.BookingOwner?.FirstName!,
                LastName = result.Result.BookingOwner?.LastName!,
                Email = result.Result.BookingOwner?.Email!,
                StreetName = result.Result.BookingOwner?.Address?.StreetName!,
                PostalCode = result.Result.BookingOwner?.Address?.PostalCode!,
                City = result.Result.BookingOwner?.Address?.City!,
                BookingDate = result.Result.BookingDate
            };

            return new BookingResult<Booking?>
            {
                Success = true,
                Result = booking
            };
        }
        return new BookingResult<Booking?>
        {
            Success = false,
            Error = "Booking not found."
        };
    }
}
