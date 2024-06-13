using Application.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace WebMemoryzoneApi.Filters
{
    public class FileValidatorFilter<T> : ActionFilterAttribute where T : class
    {
        private readonly string[] _allowedExtension;
        private readonly long _maxSize;
        private readonly Type _type;
        public FileValidatorFilter(string[] allowedExtension, long maxSize)
        {
            _allowedExtension = allowedExtension;
            _maxSize = maxSize;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.FirstOrDefault(x => x.Value is T);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("No file data provided.");
                return;
            }
            var properties = param.Value.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(IFormFile))
                {
                    var file = property.GetValue(param.Value) as IFormFile;
                    if (!(file is null) && !ValidateFile(context, file)) return;
                }
                else if (property.PropertyType == typeof(IFormFile[]))
                {
                    var files = property.GetValue(param.Value) as IFormFile[];
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            if (!(file is null) && !ValidateFile(context, file)) return;
                        }
                    }
                }
            }

            base.OnActionExecuting(context);
        }

        private bool ValidateFile(ActionExecutingContext context, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                context.Result = new BadRequestObjectResult("File is empty.");
                return false;
            }

            if (!FileValidator.IsFileExtensitonAllowed(file, _allowedExtension))
            {
                var allowedExtensionsMessage = string.Join(", ", _allowedExtension).Replace(".", "").ToUpper();
                context.Result = new BadRequestObjectResult($"Invalid file type. Please upload {allowedExtensionsMessage} file.");
                return false;
            }

            if (!FileValidator.IsFileSizeWithinLimit(file, _maxSize))
            {
                var mbSize = (double)_maxSize / 1024 / 1024;
                context.Result = new BadRequestObjectResult($"File size exceeds the maximum allowed size ({mbSize} MB).");
                return false;
            }
            return true;
        }
    }
}
