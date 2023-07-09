using FluentValidation;
using Payments.Protocol.Http.Payments;

namespace Payments.Presentation.Validation.Payment;

/// <summary>
/// Fluent Validation for PaymentIdRequest
/// </summary>
public class GetValidator : AbstractValidator<PaymentIdRequest>
{
    /// <summary>
    /// Validation rules
    /// </summary>
    public GetValidator()
    {
        RuleFor(m => m)
            .Cascade(ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop)
            .Must(x => !string.IsNullOrWhiteSpace(x.PaymentId))
            .When(x => string.IsNullOrWhiteSpace(x.PaymentExternalId)).WithMessage(
                $"Either {nameof(PaymentIdRequest.PaymentId)} or " +
                $"{nameof(PaymentIdRequest.PaymentExternalId)} should contain value")
            .Must(x => !string.IsNullOrWhiteSpace(x.PaymentExternalId))
            .When(x => string.IsNullOrWhiteSpace(x.PaymentId)).WithMessage(
                $"Either {nameof(PaymentIdRequest.PaymentId)} or " +
                $"{nameof(PaymentIdRequest.PaymentExternalId)} should contain value")
            .Must(x => string.IsNullOrWhiteSpace(x.PaymentExternalId) || string.IsNullOrWhiteSpace(x.PaymentId))
            .WithMessage($"Either {nameof(PaymentIdRequest.PaymentId)} or " +
                         $"{nameof(PaymentIdRequest.PaymentExternalId)} should contain value");
    }
}