using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Common;
using Payments.Application.Common.Enums;
using Payments.Protocol.Http.Common;
using Error = Payments.Protocol.Http.Common.Error;

namespace Payments.Presentation.Common;

/// <summary>
/// Base controller functionality
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Handle result based on chosen method
    /// </summary>
    /// <param name="handlingResult"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [NonAction]
    internal IResult HandleHttpResult(HandlingResult handlingResult)
    {
        return (handlingResult.Status) switch
        {
            HandlingResultStatus.Ok => TypedResults.Ok(BaseResponse.Success(handlingResult.Data)),
            HandlingResultStatus.Created => TypedResults.Created("", BaseResponse.Success(handlingResult.Data)),
            HandlingResultStatus.NotFound => TypedResults.NotFound(BaseResponse.Failure(handlingResult.Errors.Adapt(new List<Error>()))),
            HandlingResultStatus.BadRequest => TypedResults.BadRequest(BaseResponse.Failure(handlingResult.Errors.Adapt(new List<Error>()))),
            _ => throw new ArgumentException("Unexpected handling result", nameof(handlingResult))
        };
    }
}