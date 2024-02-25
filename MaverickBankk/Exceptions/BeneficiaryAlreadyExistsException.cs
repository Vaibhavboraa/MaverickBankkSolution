using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    internal class BeneficiaryAlreadyExistsException : Exception
    {
        public BeneficiaryAlreadyExistsException()
        {
        }

        public BeneficiaryAlreadyExistsException(string? message) : base(message)
        {
        }

        public BeneficiaryAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BeneficiaryAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
