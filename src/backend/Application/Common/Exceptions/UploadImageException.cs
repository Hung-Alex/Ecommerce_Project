
namespace Application.Common.Exceptions
{
    public class UploadImageException : Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public UploadImageException() : base()
        {
            Errors = new List<string>();
        }
        public UploadImageException(string message) : base(message) { }
        public UploadImageException(IEnumerable<string> Error) : this()
        {
            Errors = Error;
        }

    }
}
