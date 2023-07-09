using FluentValidation;
using Payments.Protocol.Http.Payments;

namespace Payments.Presentation.Validation.Payment;

/// <summary>
/// Fluent validation for CreatePaymentRequest
/// </summary>
public class CreateValidator : AbstractValidator<CreatePaymentRequest>
{
    /// <summary>
    /// Validation rules
    /// </summary>
    public CreateValidator()
    {
        RuleFor(r => r.OrderId)
            .NotNull()
            .NotEmpty()
            .WithMessage(r => $"Fild {nameof(r.OrderId)} cant be empty.");

        RuleFor(r => r.ExternalId)
            .NotNull()
            .NotEmpty()
            .WithMessage(r => $"Fild {nameof(r.ExternalId)} cant be empty.");
    }
}