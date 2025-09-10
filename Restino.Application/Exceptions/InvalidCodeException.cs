namespace Restino.Application.Exceptions
{
    public class InvalidCodeException : Exception
    {
        public InvalidCodeException()
            : base($"Two-factor code is invalid or expired.")
        {
            
        }
    }
}
