using System.Runtime.Serialization;

namespace Infrastructure.Repositories;

[Serializable]
public class InvalidGroupIdException : Exception
{
    public InvalidGroupIdException()
    {
    }

    protected InvalidGroupIdException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidGroupIdException(string? message) : base(message)
    {
    }

    public InvalidGroupIdException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}