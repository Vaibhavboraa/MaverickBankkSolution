using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    public class NoAdminFoundException : Exception
    {
        public NoAdminFoundException()
        {
        }

        public NoAdminFoundException(string? message) : base(message)
        {
        }

        public NoAdminFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoAdminFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}