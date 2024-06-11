using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebMemoryzoneApi.Filters
{
    public class FileValidatorFilter : IAsyncActionFilter
    {
        private readonly string[] _allowedExtension;
        private readonly long _maxSize;
        public FileValidatorFilter(string[] allowedExtension, long maxSize)
        {
            _allowedExtension = allowedExtension;
            _maxSize = maxSize;
        }
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(x => x.Value is IFormFile);

            var file= param.Value as IFormFile;
            if (file==null||file.Length==0)
            {
                //context.Result = new badre("");
            }
            throw new NotImplementedException();
        }
    }
}
