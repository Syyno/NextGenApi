using Payments.Application.Common.Enums;

namespace Payments.Application.Common;

public class HandlingResult
{
    public object Data { get; set; }
    public HandlingResultStatus Status { get; init; }
    public List<Error> Errors { get; set; } = new();
    private IReadOnlyCollection<HandlingResultStatus> SuccessfulStates = new[]
        { HandlingResultStatus.Ok, HandlingResultStatus.Created, HandlingResultStatus.NoContent };
    
    public bool IsSuccess => this.SuccessfulStates.Contains(this.Status);
    public bool IsFailure => !IsSuccess;
    
    public static HandlingResult Ok(object data) => new()
    {
        Status = HandlingResultStatus.Ok,
        Data = data
    };

    public static HandlingResult BadRequest(string message) => new()
    {
        Status = HandlingResultStatus.BadRequest,
        Errors = new List<Error> {new((int) HandlingResultStatus.BadRequest, message)}
    };
    
    public static HandlingResult NotFound(string message) => new()
    {
        Status = HandlingResultStatus.NotFound,
        Errors = new List<Error> {new((int) HandlingResultStatus.NotFound, message)}
    };
}