using IdentityModel.OidcClient.Browser;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AuthApp.Authorization
{
    public class WinFormsBrowser : IBrowser
    {
        private readonly Func<Form> _formFactory;

        public WinFormsBrowser(Func<Form> formFactory)
        {
            _formFactory = formFactory;
        }
        public WinFormsBrowser(string title = "Authenticating...", int width = 1024, int height = 768)
        {
            _formFactory = () => new Form
            {
                Name = "WebAuthentication",
                Text = title,
                Width = width,
                Height = height
            };
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<BrowserResult>();
            Form window = _formFactory();
            var webView = new WebView2
            {
                Dock = DockStyle.Fill
            };
            webView.NavigationStarting += delegate (object? sender, CoreWebView2NavigationStartingEventArgs e)
            {
                if (e.Uri.StartsWith(options.EndUrl))
                {
                    tcs.SetResult(new BrowserResult
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Uri.ToString()
                    });
                    window.Close();
                }
            };
            window.Closing += delegate
            {
                if (!tcs.Task.IsCompleted)
                {
                    tcs.SetResult(new BrowserResult
                    {
                        ResultType = BrowserResultType.UserCancel
                    });
                }
            };
            await webView.EnsureCoreWebView2Async();
            window.Controls.Add(webView);
            window.Show();
            webView.CoreWebView2.Navigate(options.StartUrl);
            return await tcs.Task;
        }
    }
}
