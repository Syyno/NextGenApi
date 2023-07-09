using Mapster;
using Payments.Application.UseCases.Payment.Command.Create;
using Payments.Protocol.Http.Common;
using Payments.Protocol.Http.Payments;

namespace Payments.Presentation.Mappings;

public class MapsterConfigs
{
    public class PaymentMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Protocol.Http.Payments.CreatePaymentRequest, Create>()
                .Map(dest => dest.Payment.OrderId, src => src.OrderId)
                .Map(dest => dest.Payment.ExternalId, src => src.ExternalId)
                .Map(dest => dest.Payment.PaymentSum, src => src.PaymentSum)
                .Map(dest => dest.Payment.PaymentType, src => src.PaymentType);
            
            config.ForType<Domain.Entity.Payment.Payment, Payment>()
                .Map(dest => dest.OrderId, src => src.OrderId.Value)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.ExternalId, src => src.ExternalId.Value)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.PaymentSum, src => src.PaymentSum.Value)
                .Map(dest => dest.PaymentType, src => src.PaymentType.Value);

            config.ForType<Application.Common.Error, Error>()
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.ErrorCode, src => src.ErrorCode);
        }
    }
}