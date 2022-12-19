using AuthApp.Authorization;
using IdentityModel.OidcClient;

namespace AuthApp
{
    public partial class Form1 : Form
    {
        private AuthClient _authClient;
        private UserIdentity _userIdentity;
        public Form1()
        {
            InitializeComponent();
            _authClient = new AuthClient(new OktaClientOptions());
        }

        private async void OnLoad(object sender, EventArgs e)
        {
            _userIdentity = await _authClient.Login();
        }
    }
}