using System.Runtime.Serialization;

namespace MaverickBankk.Exceptions
{
    [Serializable]
    public class InvalidUserException : Exception
    {
        public InvalidUserException()
        {
        }

        public InvalidUserException(string? message) : base("Invalid username or password")
        {
        }


    }
}