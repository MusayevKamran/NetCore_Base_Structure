namespace App.Application.Exceptions;

/// <summary>
///     Not found exception
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string? message) : base(message) {}
}