using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
        Console.WriteLine("Received booking create request:");
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(request));

        if (!ModelState.IsValid)
        {
            Console.WriteLine("Model state invalid.");
            return BadRequest(ModelState);
        }

        var result = await _bookingService.CreateBookingAsync(request);

        Console.WriteLine($"Booking result: Success={result.Success}, BookingId={result.Booking?.Id}, EventId={result.Booking?.EventId}");
        return result.Success
            ? Ok(new { result.Booking!.Id })
            : StatusCode(StatusCodes.Status500InternalServerError, "Unable to create booking.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(string id)
    {
        Console.WriteLine($"Received booking get request for id: {id}");

        if (string.IsNullOrEmpty(id))
        {
            Console.WriteLine("Booking ID is null or empty.");
            return BadRequest(new { error = "Booking ID cannot be null or empty." });
        }
        var result = await _bookingService.GetBookingAsync(id);
        Console.WriteLine($"GetBookingAsync result: Success={result.Success}, Result={System.Text.Json.JsonSerializer.Serialize(result.Result)}");
        return result.Success && result.Result != null
            ? Ok(result.Result)
            : NotFound(new { error = "Booking not found." });
    }
}