namespace NetCoreWithRedis.Core.Helper.ExceptionHelper
{
    public class ExceptionMessageHelper
    {
        public const string UnexpectedSystemError = "Unexpected System Error";
        public const string UnexpectedDBError = "Unexpected DB Error";
        public const string FattalSystemError = "Fattal System Error";
        public const string UnexpectedCacheError = "Unexpected Cache Error";
        public static string CannotEmptyField(string FieldName) => string.Format("Some field is empty. Those fields are", FieldName);
        //public static string CannotEmptyFields(string FieldNames) => string.Format("Some field is empty. Those fields are", FieldNames);
        public static string IsInUse(string FieldName) => string.Format("Can not insert {0} field. Because it is in use!", FieldName);
        public static string DataNotFound(string FieldName) => string.Format("{0} not found", FieldName);
        public static string DataNotMatch(string FieldName) => string.Format("Does not match {0}", FieldName);
        public static string UnauthorizedAccess(string parmUserName) => string.Format("Unauthorized Access by User Name : {0}", parmUserName);
    }
}
