using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace AuthApp.Authorization
{
    public class OktaClientOptions : IOidcClientOptionsBuilder
    {
        private string Authority => "https://" + Properties.Settings.Default.Auth0Domain;
        public OidcClientOptions Options => new()
        {
            Authority = Authority,
            ClientId = Properties.Settings.Default.Auth0ClientId,
            Scope = "openid profile email offline_access roles",
            RedirectUri = Authority + "/mobile",
            Browser = new WinFormsBrowser(),
            ClockSkew = TimeSpan.FromMinutes(5.0),
            Policy = new Policy
            {
                RequireAccessTokenHash = false
            }
        };

        public IEnumerable<KeyValuePair<string, string>> LoginParameters => new Dictionary<string, string>
        {
            { "audience", "test-app" }
        };
    }
}
