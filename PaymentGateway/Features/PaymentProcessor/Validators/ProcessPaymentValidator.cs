using Application.Features.PaymentProcessor.Models;
using FluentValidation;

namespace Application.Features.PaymentProcessor.Validators
{
    public class ProcessPaymentValidator : AbstractValidator<ProcessPaymentCommand>
    {
        public ProcessPaymentValidator()
        {
            RuleFor(payment => payment.Cvv).Length(3, 4);

            RuleFor(payment => payment.CardNumber).Length(16);

            RuleFor(payment => payment.ExpiryMonth).GreaterThan(0).LessThanOrEqualTo(12);

            RuleFor(payment => payment.ExpiryYear).GreaterThanOrEqualTo(DateTime.Now.Year).LessThan(10000);

            RuleFor(payment => payment.Amount).GreaterThan(0);
        }
    }
}
