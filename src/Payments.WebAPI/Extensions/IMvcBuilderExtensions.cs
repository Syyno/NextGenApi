using Microsoft.AspNetCore.Mvc;
using Payments.Protocol.Http.Common;

namespace Payments.WebAPI.Extensions;

public static class IMvcBuilderExtensions
{
    public static IMvcBuilder AddModelStateValidation(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray());
                
                var errorResponse = errorsInModelState.SelectMany(e => e.Value).Select(e => new Error(400, e)).ToList();

                return new BadRequestObjectResult(BaseResponse.Failure(errorResponse));
            };
        });
        return builder;
    }
}