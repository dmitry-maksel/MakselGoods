namespace Identity.API.Core.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message)
            : base(message)
        {

        }
    }
}
