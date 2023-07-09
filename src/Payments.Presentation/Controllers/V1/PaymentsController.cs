using Asp.Versioning;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Common;
using Payments.Application.UseCases.Payment.Command.Create;
using Payments.Application.UseCases.Payment.Command.Delete;
using Payments.Application.UseCases.Payment.Query.Read;
using Payments.Presentation.Common;
using Payments.Presentation.Models;
using Payments.Protocol.Http.Common;
using Payments.Protocol.Http.Payments;
using Error = Payments.Protocol.Http.Common.Error;
using ProtocolPayment = Payments.Protocol.Http.Payments.Payment;

namespace Payments.Presentation.Controllers.V1;

/// <summary>
/// CRUD Operations for payments
/// </summary>
[Route("api/v1/[controller]/")]
[ApiVersion("1.0")]
public class PaymentsController : BaseController
{
    private IMediator _mediator;

    /// <summary>
    /// Ctor for DI
    /// </summary>
    /// <param name="mediator"></param>
    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать платеж
    /// </summary>
    /// <param name="request">Запрос на создание платежа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный платеж.</returns>
    /// <response code="200">Успешно.</response>
    /// <response code="400">Ошибка в данных запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BaseResponse<ProtocolPayment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> CreateAsync(
        [FromBody] CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<Create>(), cancellationToken);
        return HandleHttpResult(AdaptResult(result));
    }
    
    
    /// <summary>
    /// Получить платеж по внутреннему или по внешнему айди
    /// </summary>
    /// <param name="idRequest">Запрос на получение платежа</param>
    /// <param name="cancellationToken">Токен отмены</param
    /// <returns>Данные по платежу.</returns>
    /// <response code="200">Успешно.</response>
    /// <response code="400">Ошибка в данных запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BaseResponse<ProtocolPayment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetAsync(
        [FromQuery] PaymentIdRequest idRequest,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(idRequest.Adapt<Read>(), cancellationToken);
        return HandleHttpResult(AdaptResult(result));
    }
    
    /// <summary>
    /// Удалить платеж по внутреннему или по внешнему айди
    /// </summary>
    /// <param name="idRequest">Запрос на удаление платежа</param>
    /// <param name="cancellationToken">Токен отмены</param
    /// <returns>Удаленый платеж.</returns>
    /// <response code="200">Успешно.</response>
    /// <response code="400">Ошибка в данных запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BaseResponse<ProtocolPayment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> DeleteAsync(
        [FromQuery] PaymentIdRequest idRequest,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(idRequest.Adapt<Delete>(), cancellationToken);
        return HandleHttpResult(AdaptResult(result));
    }

    /// <summary>
    /// Обновить данные по платежу
    /// </summary>
    /// <param name="idRequest">Айди платежа</param>
    /// <param name="request">Информация по платежу для апдейта</param>
    /// <param name="cancellationToken">Токен отмены</param
    /// <returns>Обновленный платеж.</returns>
    /// <response code="200">Успешно.</response>
    /// <response code="400">Ошибка в данных запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BaseResponse<ProtocolPayment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UpdateAsync(
        [FromQuery] PaymentIdRequest idRequest,
        [FromBody] UpdatePaymentInternalRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.MapToCommand(idRequest), cancellationToken);
        return HandleHttpResult(AdaptResult(result));
    }

    private HandlingResult AdaptResult(HandlingResult result)
    {
        if (result.IsSuccess)
            result.Data.Adapt<Payment>();
        else
            result.Errors.Adapt<Error>();

        return result;
    }
}