using System;
using System.Threading.Tasks;
using App.iOS.CustomViews;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using Shared.Services;
using Shared.ViewModels;
using Shell.Views;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPopupView), typeof(LoginPopupRenderer))]
namespace App.iOS.CustomViews
{
    class LoginPopupRenderer : PageRenderer
    {
        private bool _isShown;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (_isShown) return;
            _isShown = true;

            var org = ((Element as LoginPopupView)?.BindingContext as LoginPopupViewModel)?.NavigationParameter as string;
            // Initialize the object that communicates with the OAuth service
            var auth = new OAuth2Authenticator(
              Constants.ClientId,
              Constants.Scope,
              new Uri(string.Format(Constants.AuthorizeUrl, org)),
              new Uri(Constants.CallbackUrl));

            // Register an event handler for when the authentication process completes
            auth.Completed += OnAuthenticationCompleted;

            // Display the UI
            PresentViewController(auth.GetUI(), true, null);
        }

        async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                await FetchUserInfo(e.Account);
                IoC.Get<IAuthenticationService>().SetAccount(e.Account);
                IoC.Get<IApplicationNavigationService>().To<RootViewModel>();
            }
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