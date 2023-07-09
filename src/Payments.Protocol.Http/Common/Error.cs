namespace Payments.Protocol.Http.Common;

public class Error
{
    public Error(int errorCode, string message)
    {
        this.ErrorCode = errorCode;
        this.Message = message;
    }
    
    public int ErrorCode { get; }
    public string Message { get; }
}