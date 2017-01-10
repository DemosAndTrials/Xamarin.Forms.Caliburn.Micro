using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using App.UWP.CustomViews;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using Shared.Services;
using Shared.ViewModels;
using Shell.Views;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(LoginPopupView), typeof(LoginPopupRenderer))]
namespace App.UWP.CustomViews
{
    class LoginPopupRenderer : PageRenderer
    {
        protected override async void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            };

            var org = ((Element as LoginPopupView)?.BindingContext as LoginPopupViewModel)?.NavigationParameter as string;
            // Initialize the object that communicates with the OAuth service
            // Display the UI
            var code = await AuthenticateUsingWebAuthenticationBroker(org);
            if (!string.IsNullOrEmpty(code))
            {
                var account = ConvertCodeToAccount(code);
                await FetchUserInfo(account);
                IoC.Get<IAuthenticationService>().SetAccount(account);
                IoC.Get<IApplicationNavigationService>().To<RootViewModel>();
            }
        }

        private async Task<string> AuthenticateUsingWebAuthenticationBroker(string org)
        {
            var sfdcUrl = string.Format(Constants.AuthorizeUrl, org) + "?client_id=" +
                            Uri.EscapeDataString(Constants.ClientId);
            sfdcUrl += "&display=page";
            sfdcUrl += "&redirect_uri=" + Uri.EscapeDataString(Constants.CallbackUrl);
            sfdcUrl += "&response_type=token";
            sfdcUrl += "&scope=" + Uri.EscapeDataString(Constants.Scope);

            var startUri = new Uri(sfdcUrl);

            var webAuthenticationResult =
              await
                WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri,
                  new Uri(Constants.CallbackUrl));
            return webAuthenticationResult.ResponseStatus != WebAuthenticationStatus.Success ? null : webAuthenticationResult.ResponseData;
        }

        private Account ConvertCodeToAccount(string code)
        {
            var responseUri = new Uri(code);
            string[] parameters = responseUri.Fragment.Substring(1).Split('&');

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var parameter in parameters)
            {
                string[] parts = parameter.Split('=');
                string name = Uri.UnescapeDataString(parts[0]);
                string value = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : "";
                values.Add(name, value);
            }
            return new Account(null, values);
        }

        public async Task FetchUserInfo(Account account)
        {
            var request = new OAuth2Request("GET", new Uri(account.Properties["id"]), null, account);
            var response = await request.GetResponseAsync();
            if (response != null)
            {
                var userJson = response.GetResponseText();
                var user = JObject.Parse(userJson);
                account.Username = (string)user[Constants.UsernameAccountProperty];
                account.Properties[Constants.EmailAccountProperty] = (string)user[Constants.EmailAccountProperty];
                account.Properties[Constants.PhotoAccountProperty] = (string)user[Constants.PhotoAccountProperty]["picture"];
            }
        }
    }
}
