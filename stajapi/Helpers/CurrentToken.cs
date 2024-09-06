using System.IdentityModel.Tokens.Jwt;

namespace stajapi.Helpers
{
    public static class CurrentToken
    {
        public static string Token {  get; set; }
        public static DateTime tokenExpireDate { get; set; }
    }
}
