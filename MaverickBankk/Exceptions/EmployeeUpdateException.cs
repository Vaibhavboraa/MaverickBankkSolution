﻿using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
   public class EmployeeUpdateException : Exception
    {
        public EmployeeUpdateException()
        {
        }

        public EmployeeUpdateException(string? message) : base(message)
        {
        }

        public EmployeeUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmployeeUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
