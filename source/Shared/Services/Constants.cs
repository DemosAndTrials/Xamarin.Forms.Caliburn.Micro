namespace Shared.Services
{
    public static class Constants
    {
        // https://developer.salesforce.com/docs/atlas.en-us.api_streaming.meta/api_streaming/code_sample_auth_oauth.htm 

        public const string CallbackUrl = "sfdc://success";
        public const string ClientId = "3MVG9HxRZv05HarQ85GEfSOEm4swEALjm8Wvx9W.pE.p1xGdZ_0ukMo_dSm88s4M74bWZBufQuFdiWfkVqhxD";

        public const string Scope = "api chatter_api refresh_token full";
        public const string AuthorizeUrl = "https://{0}.salesforce.com/services/oauth2/authorize";
        public const string AccessTokenUrl = "https://{0}.salesforce.com/services/oauth2/token";
        public const string UserInfoUrl = "https://{0}.salesforce.com/services/oauth2/userinfo";

        public const string UsernameAccountProperty = "username";
        public const string EmailAccountProperty = "email";
        public const string PhotoAccountProperty = "photos";
    }
}
