using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    public class NoBanksFoundException : Exception
    {
        public NoBanksFoundException()
        {
        }

        public NoBanksFoundException(string? message) : base(message)
        {
        }

        public NoBanksFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoBanksFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}