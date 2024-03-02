using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    public class BankEmployeeCreationException : Exception
    {
        public BankEmployeeCreationException()
        {
        }

        public BankEmployeeCreationException(string? message) : base(message)
        {
        }

        public BankEmployeeCreationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BankEmployeeCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
