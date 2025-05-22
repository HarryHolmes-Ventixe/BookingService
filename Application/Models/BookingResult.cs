namespace Application.Models;

public class BookingResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
}

public class EventResult<T> : BookingResult
{
    public T? Result { get; set; }
}
