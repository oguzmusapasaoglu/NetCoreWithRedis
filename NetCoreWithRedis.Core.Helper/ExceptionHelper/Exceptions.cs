using System;

namespace NetCoreWithRedis.Core.Helper.ExceptionHelper
{
    public class KnownException : Exception
    {
        public string ExceptionMessage { get; set; }
        public ErrorTypeEnum ErrorTypeProp { get; set; }
        public Exception ExceptionProp { get; set; }
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
        public KnownException(ErrorTypeEnum exceptionType, string message, Exception exception)
        {
            ExceptionMessage = message;
            ErrorTypeProp = exceptionType;
            exception = exception;
        }
    }
    public class RequestWarningException : KnownException
    {
        public RequestWarningException(ErrorTypeEnum errorTypeProp, string exceptionCode, string message) : base(message)
        {
            ErrorTypeProp = errorTypeProp;
            ExceptionCode = exceptionCode;
            ExceptionMessage = message;
        }
        public RequestWarningException(ErrorTypeEnum errorTypeProp, string exceptionCode, string message, Exception exception) : base(message)
        {
            ErrorTypeProp = errorTypeProp;
            ExceptionMessage = message;
            ExceptionCode = exceptionCode;
            ExceptionProp = exception;
        }

        public string ExceptionCode { get; set; }
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
