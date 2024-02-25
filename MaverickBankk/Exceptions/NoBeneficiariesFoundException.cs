using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    internal class NoBeneficiariesFoundException : Exception
    {
        public NoBeneficiariesFoundException()
        {
        }

        public NoBeneficiariesFoundException(string? message) : base(message)
        {
        }

        public NoBeneficiariesFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoBeneficiariesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
