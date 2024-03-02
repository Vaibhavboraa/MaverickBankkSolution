using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable] 
    public class AccountFetchException : Exception
    {
        public AccountFetchException()
        {
        }

        public AccountFetchException(string? message) : base(message)
        {
        }

        public AccountFetchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AccountFetchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
