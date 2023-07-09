using Payments.Protocol.Http.Common;
using Payments.Protocol.Http.Payments;
using Refit;

namespace Payments.Protocol.Http.Client;

public interface IPaymentsApiHttpClient
{
    /// <summary>
    /// Создать платеж
    /// </summary>
    /// <param name="request">Запрос на создание платежа</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Созданный платеж.</returns>
    [Post(Uris.Payments_v1.Create)]
    public Task<IApiResponse<BaseResponse<Payment>>> CreateAsync(
        CreatePaymentRequest request,
        CancellationToken token);
    
     
    /// <summary>
    /// Получить платеж по внутреннему или по внешнему айди
    /// </summary>
    /// <param name="id">Запрос на получение платежа</param>
    /// <param name="token">Токен отмены</param
    /// <returns>Данные по платежу.</returns>
    [Get(Uris.Payments_v1.Read)]
    public Task<IApiResponse<BaseResponse<Payment>>> ReadAsync(
        [Query]PaymentIdRequest id,
        CancellationToken token);

    /// <summary>
    /// Обновить данные по платежу
    /// </summary>
    /// <param name="id">Айди платежа</param>
    /// <param name="request">Информация по платежу для апдейта</param>
    /// <param name="token">Токен отмены</param
    /// <returns>Обновленный платеж.</returns>
    [Patch(Uris.Payments_v1.Update)]
    public Task<IApiResponse<BaseResponse<Payment>>> UpdateAsync(
        UpdatePaymentRequest request, 
        [Query]PaymentIdRequest id,
        CancellationToken token);
    
    /// <summary>
    /// Удалить платеж по внутреннему или по внешнему айди
    /// </summary>
    /// <param name="id">Запрос на удаление платежа</param>
    /// <param name="token">Токен отмены</param
    /// <returns>Удаленый платеж.</returns>
    [Delete(Uris.Payments_v1.Delete)]
    public Task<IApiResponse<BaseResponse<Payment>>> DeleteAsync(
        [Query]PaymentIdRequest id,
        CancellationToken token);
}