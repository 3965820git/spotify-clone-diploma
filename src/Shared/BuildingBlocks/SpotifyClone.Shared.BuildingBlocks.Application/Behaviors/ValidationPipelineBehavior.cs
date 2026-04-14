using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors.Helpers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators ?? throw new ArgumentNullException(nameof(validators));

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        if (!_validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        IEnumerable<Task<ValidationResult>> validationTasks = _validators
            .Select(v => v.ValidateAsync(context, cancellationToken));

        ValidationResult[] results = await Task.WhenAll(validationTasks);

        Error[] errors = results
            .SelectMany(r => r.Errors)
            .ToErrors();

        if (errors.Length > 0)
        {
            return ResultFactory.CreateFailureResult<TResponse>(errors);
        }

        return await next(cancellationToken);
    }
}
