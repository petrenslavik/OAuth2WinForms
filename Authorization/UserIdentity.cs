using IdentityModel.OidcClient;
using System.Security.Claims;

namespace AuthApp.Authorization
{
    public class UserIdentity
    {
        private readonly ClaimsIdentity userPrincipal;
        public UserIdentity(ClaimsIdentity principal, AccessToken accessToken)
        {
            userPrincipal = principal;
            AccessToken = accessToken;
        }

        public string? Email => userPrincipal.FindFirst("test")?.Value;

        public AccessToken AccessToken { get; }
    }
}
