using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    internal class BeneficiaryServiceException : Exception
    {
        public BeneficiaryServiceException()
        {
        }

        public BeneficiaryServiceException(string? message) : base(message)
        {
        }

        public BeneficiaryServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BeneficiaryServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
