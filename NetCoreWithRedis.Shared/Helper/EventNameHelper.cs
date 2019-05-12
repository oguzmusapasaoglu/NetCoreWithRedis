namespace NetCoreWithRedis.Shared.Helper
{
    public class EventNameHelper
    {
        #region A
        public const string ApplicationMapCreateOrUpdate = "ApplicationMapCreateOrUpdate";
        public const string ApplicationMapFilter = "ApplicationMapFilter";
        public const string ApplicationMapSingleById = "ApplicationMapSingleById";
        public const string ApplicationMapFilterByUserId = "ApplicationMapFilterByUserId";
        public const string AuthorizationGroupCreateOrUpdate = "AuthorizationGroupCreateOrUpdate";
        public const string AuthorizationGroupFilter = "AuthorizationGroupFilter";
        public const string AuthorizationGroupSingleById = "AuthorizationGroupSingleById";
        #endregion

        #region C

        #endregion

        #region G
        public const string GetTextTranslationByLang = "GetTextTranslationByLang";
        public const string GetAllLanguage = "GetAllLanguage";
        public const string GetAllDartType = "GetAllDartType";
        public const string GetAllDistributionAwardsType = "GetAllDistributionAwardsType";
        public const string GetAllAuthorizationGroup = "GetAllAuthorizationGroup";
        #endregion

        #region L
        public const string LanguagesFilter = "LanguagesFilter";
        public const string CreateOrUpdateLanguages = "CreateOrUpdateLanguages";
        #endregion

        #region S
        public const string SiteTextsCreateOrUpdate = "SiteTextsCreateOrUpdate";
        public const string SiteTextsFilter = "SiteTextsFilter";
        public const string SiteTextsSingleById = "SiteTextsSingleById";
        public const string ServicesAuthorizationCreateOrUpdate = "ServicesAuthorizationCreateOrUpdate";
        public const string ServicesNamesCreateOrUpdate = "ServicesNamesCreateOrUpdate";
        public const string ServicesAuthorizationFilter = "ServicesAuthorizationFilter";
        public const string ServicesAuthorizationSingle = "ServicesAuthorizationSingle";
        public const string ServicesFilter = "ServicesFilter";
        public const string ServicesNameSingleById = "ServicesNameSingleById";
        public const string ServicesNameSingleByName = "ServicesNameSingleByName";

        #endregion

        #region T
        public const string TextTranslationCreateOrUpdate = "TextTranslationCreateOrUpdate";
        public const string TextTranslationFilter = "TextTranslationFilter";
        public const string TextTranslationSingleByTextId = "TextTranslationSingleByTextId";
        #endregion

        #region U
        public const string UserLogin = "UserLogin";
        public const string UserCreateOrUpdate = "UserCreateOrUpdate";
        public const string UserSingleById = "UserSingleById";
        public const string UserFilter = "UserFilter";
        public const string UserChancePassword = "UserChancePassword";
        public const string UserForgotPassword = "UserForgotPassword";
        public const string UsersApplicationMapCreateOrUpdate = "UsersApplicationMapCreateOrUpdate";
        //public const string UsersApplicationMapByUserId = "UsersApplicationMapByUserId";
        //public const string UsersApplicationMapFilter = "UsersApplicationMapFilter";
        #endregion
    }
}
