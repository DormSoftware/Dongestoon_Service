using System.Runtime.Serialization;

namespace Infrastructure.Repositories.Exceptions;

[Serializable]
public class NoProductFoundWithGivenException : Exception
{
    public NoProductFoundWithGivenException(Guid productId) : base($"There is no Product with Given Id ({productId})")
    {
    }

    protected NoProductFoundWithGivenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoProductFoundWithGivenException(string? message) : base(message)
    {
    }

    public NoProductFoundWithGivenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}