namespace NetCoreWithRedis.Core.Helper.ExceptionHelper
{
    public class ExceptionCodeHelper
    {
        public static string CannotEmptyField = "Cannot Empty Field";
        public static string IsInUse = "Is In Use";
        public static string DataNotFound = "Data Not Found";
        public static string DataNotMatch = "Data Not Match";
    }
    public class AuthorizationExceptionCodes
    {
        public const string TokenNotfound = "Token Not found";
        public const string TokenExpired = "Token Expired";
        public const string UnauthorizationAccess = "Unauthorization Access";
    }
}
