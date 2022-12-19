using IdentityModel.OidcClient;
using System.Security.Claims;
using IdentityModel.Client;

namespace AuthApp.Authorization
{
    public class AuthClient
    {
        private readonly OidcClient _oAuthClient;
        private readonly Parameters _loginParameters;
        public AuthClient(IOidcClientOptionsBuilder builder)
        {
            _oAuthClient = new OidcClient(builder.Options);
            _loginParameters = new Parameters(builder.LoginParameters);
        }

        public async Task<UserIdentity> Login()
        {
            var refreshToken = Properties.Settings.Default.RefreshToken;
            if (true || string.IsNullOrWhiteSpace(refreshToken))
            {
                var loginResult = await _oAuthClient.LoginAsync(new LoginRequest()
                {
                    FrontChannelExtraParameters = _loginParameters
                });

                Properties.Settings.Default.RefreshToken = loginResult.RefreshToken;
                Properties.Settings.Default.Save();

                return new UserIdentity(loginResult.User.Identity as ClaimsIdentity, new AccessToken(loginResult.AccessToken, loginResult.AccessTokenExpiration));
            }

            var accessTokenResult = await _oAuthClient.RefreshTokenAsync(refreshToken);
            var userInfoResult = await _oAuthClient.GetUserInfoAsync(accessTokenResult.AccessToken);

            return new UserIdentity(new ClaimsIdentity(userInfoResult.Claims, "refresh_token", "name", "role"), new AccessToken(accessTokenResult.AccessToken, accessTokenResult.AccessTokenExpiration));
        }
    }
}
