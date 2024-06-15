using Microsoft.AspNetCore.Identity;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One more a validation failures occorred")
        {
            Error = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<IdentityError> errors)
        {
            Error = errors.GroupBy(x => x.Code, x => x.Description)
                .ToDictionary(group => group.Key, group => group.ToArray());
        }
        public Dictionary<string, string[]> Error { get; private set; }
    }
}
