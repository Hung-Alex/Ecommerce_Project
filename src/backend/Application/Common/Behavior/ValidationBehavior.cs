using Domain.Behavior;
using FluentValidation;
using MediatR;

namespace Application.Common.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IValidatableRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validators = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            if (request is IValidatableRequest)
            {
                if (_validators.Any())
                {
                    var context = new ValidationContext<TRequest>(request);
                    var validationResult = await Task.WhenAll(
                        _validators.Select(v => v.ValidateAsync(context, cancellationToken)))
                        .ConfigureAwait(false);
                    var failures = validationResult.Where(r => r.Errors.Count() > 0)
                        .SelectMany(r => r.Errors)
                        .ToList();
                    if (failures.Count > 0)
                        throw new FluentValidation.ValidationException(failures);
                }
                else
                {
                    return await next();
                }
            }
            return await next().ConfigureAwait(false);
        }
    }
}
