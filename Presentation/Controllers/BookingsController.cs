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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _bookingService.CreateBookingAsync(request);
        return result.Success
            ? Ok(new { result.Booking!.Id })
            : StatusCode(StatusCodes.Status500InternalServerError, "Unable to create booking.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest(new { error = "Booking ID cannot be null or empty." });
        }
        var result = await _bookingService.GetBookingAsync(id);
        return result.Success && result.Result != null
            ? Ok(result.Result)
            : NotFound(new { error = "Booking not found." });
    }

    [HttpGet]
    public async Task<IActionResult> GetBookingsByEmail([FromQuery] string email)
    {
        var result = await _bookingService.GetBookingsByEmailAsync(email);
        return Ok(result.Result);
    }
}