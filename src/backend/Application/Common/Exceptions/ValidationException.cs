
namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One more a validation failures occorred")
        {
            Error = new Dictionary<string, string[]>();
        }
        public ValidationException(string message) : base(message) 
        {

        }
        public ValidationException(Dictionary<string, string[]> error) : this()
        {
            Error = error;
        }
        public Dictionary<string, string[]> Error { get; private set; }
    }
}
