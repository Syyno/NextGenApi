namespace Payments.Protocol.Http.Common;

public class BaseResponse
{
    /// <summary>
    /// Результат выполения
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Ошиька выполнения
    /// </summary>
    public List<Error> Errors { get; init; }
    
    public BaseResponse(bool isSuccess, List<Error> errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static BaseResponse Success() => new BaseResponse(true);
    public static BaseResponse Failure(List<Error> errors) => new BaseResponse(false, errors);
    
    public static BaseResponse Success<TData>(TData data) => new BaseResponse<TData>(true, data);
}

public class BaseResponse<TData> : BaseResponse
{
    /// <summary>
    /// Данные ответа
    /// </summary>
    public TData Data { get; init; }

    public BaseResponse(bool isSuccess, TData data, List<Error> errors = null) : base(isSuccess, errors)
    {
        Data = data;
    }
}