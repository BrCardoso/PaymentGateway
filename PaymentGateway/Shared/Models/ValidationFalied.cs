using FluentValidation.Results;

namespace Application.Shared.Models
{
    public record ValidationFalied(IEnumerable<ValidationFailure> Errors)
    {
        public ValidationFalied(ValidationFailure error) : this ([error])
        { }
    }
}
