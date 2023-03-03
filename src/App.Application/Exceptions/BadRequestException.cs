using System.Runtime.Serialization;

namespace App.Application.Exceptions;

/// <summary>
///     Throw new exception when we have incorrect request data
/// </summary>
[Serializable]
public class BadRequestException : Exception
{
    /// <inheritdoc />
    public BadRequestException()
    {
    }

    /// <inheritdoc />
    public BadRequestException(string message) : base(message)
    {
    }

    /// <inheritdoc />
    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Constructor to ensure correct serialization/deserialization
    /// </summary>
    /// <param name="info">Data to serialize</param>
    /// <param name="context">Serialization thread context</param>
    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}