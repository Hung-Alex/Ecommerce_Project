
namespace Domain.Shared
{
    public class Result<T>
    {
        public T Data { get; set; }
        protected Result(T data)
        {
            Data = data;
        }
        protected Result() { }
        public IEnumerable<Error>? Errors { get; set; }
        public bool IsSuccess { get; protected set; }
        public static Result<T> ResultSuccess(T data)
        {
            return new Result<T>(data) { IsSuccess = true };
        }
        public static Result<T> ResultFailures(params Error[] errors)
        {
            return new Result<T>() { IsSuccess = false, Errors = errors };
        }
        public static Result<T> ResultFailures(IEnumerable<Error> errors)
        {
            return new Result<T>() { IsSuccess = false, Errors = errors };
        }
    }
}
