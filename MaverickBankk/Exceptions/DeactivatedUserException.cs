﻿namespace MaverickBankk.Exceptions
{
    public class DeactivatedUserException : Exception
    {
        public DeactivatedUserException() : base("User deactivated")
        {
        }
    }
}
