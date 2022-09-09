namespace Infrastructure.Common;

public class ActionReport
{
    public bool IsSuccessful => Code == 0;
    public ReportErrorCode Code { get; set; }
    public Exception Exception { get; set; } = null!;

    public static ActionReport Error(ReportErrorCode errorCode = ReportErrorCode.InternalServerError, Exception exception = null!) => new()
    {
        Code = errorCode,
        Exception = exception
    };

    public static ActionReport Success() => new()
    {
        Code = ReportErrorCode.Successful,
    };
}

public class ActionReport<T> : ActionReport
{
    public T Output { get; set; } = default!;

    public new static ActionReport<T> Error(ReportErrorCode errorCode = ReportErrorCode.InternalServerError, Exception exception = null) => new()
    {
        Code = errorCode,
        Exception = exception
    };

    public static ActionReport<T> Success(T output) => new()
    {
        Code = ReportErrorCode.Successful,
        Output = output
    };
}

public enum ReportErrorCode
{
    Successful = 0,
    InternalServerError = 500,
    BadRequest = 400,
    NotFound = 404,
    Conflict = 409,
    Forbidden = 403,
    Unauthorized = 401,
    UnavailableForLegalReasons = 451,
    ImATeapot = 418,
    NotAcceptable = 406,
}
