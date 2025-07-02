using System.Runtime.Serialization;

namespace LangVoyageServer.Exceptions;

[Serializable]
public class UserNotFoundException : Exception
{
    public int UserId { get; }

    public UserNotFoundException(int userId) : base($"User with ID {userId} was not found.")
    {
        UserId = userId;
    }

    public UserNotFoundException(int userId, string message) : base(message)
    {
        UserId = userId;
    }

    public UserNotFoundException(int userId, string message, Exception innerException) : base(message, innerException)
    {
        UserId = userId;
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        UserId = info.GetInt32(nameof(UserId));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(UserId), UserId);
    }
}