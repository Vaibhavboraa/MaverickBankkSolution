using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    public class NoCustomersFoundException : Exception
    {
        public NoCustomersFoundException()
        {
        }

        public NoCustomersFoundException(string? message) : base(message)
        {
        }

        public NoCustomersFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoCustomersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}