using FluentValidation.Results;

namespace Application.Shared.Models
{
    public struct Result<TValue>
    {
        public bool IsError { get; }
        public bool IsSuccess => !IsError;

        public TValue? Value { get; }

        public ValidationFalied? Errors { get; }

        public Result(TValue value)
        {
            IsError = false;
            Value = value;
            Errors = default;
        }

        public Result(ValidationFalied error)
        {
            IsError = true;
            Value = default;
            Errors = error;
        }
        public Result(string propertyName, string errorMessage)
        {
            IsError = true;
            Value = default;
            Errors = new ValidationFalied(
                    new ValidationFailure(propertyName, errorMessage)
                );
        }
    }
}
