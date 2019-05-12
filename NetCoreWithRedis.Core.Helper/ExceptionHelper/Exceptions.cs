using System;

namespace NetCoreWithRedis.Core.Helper.ExceptionHelper
{
    public class KnownException : Exception
    {
        public string ExceptionMessage { get; set; }
        public ErrorTypeEnum ErrorTypeProp { get; set; }
        public Exception exception { get; set; }
        public KnownException(string message)
            : base(message)
        {
        }
        public KnownException(ErrorTypeEnum exceptionType, string message, string exceptionMessage)
            : base(message)
        {
            ExceptionMessage = exceptionMessage;
            ErrorTypeProp = exceptionType;
        }
        public KnownException(ErrorTypeEnum exceptionType, string message, Exception innerException)
        {
            ExceptionMessage = message;
            ErrorTypeProp = exceptionType;
            exception = innerException;
        }
    }
    public class RequestWarningException : KnownException
    {
        public RequestWarningException(ErrorTypeEnum exceptionType, string exceptionCode, string message) : base(message)
        {
            ExceptionType = exceptionType;
            ExceptionCode = exceptionCode;
            ExMessage = message;
        }
        public RequestWarningException(ErrorTypeEnum exceptionType, string exceptionCode, string message, Exception innerException) : base(message)
        {
            ExceptionType = exceptionType;
            ExMessage = message;
            ExceptionCode = exceptionCode;
            InnerException = innerException;
        }

        public string ExceptionCode { get; set; }
        public string ExMessage { get; set; }
        public new Exception InnerException { get; set; }
        public ErrorTypeEnum ExceptionType { get; set; }
    }
    public enum ErrorTypeEnum
    {
        GeneralExeption,
        CacheExpireExeption,
        CacheEndExeption,
        CacheNotExsistExeption,
        CahceGeneralException,
        UnexpectedExeption,
        DbOperationException,
        TokenException,
        AuthorizeException,
        WarningException
    }
}
