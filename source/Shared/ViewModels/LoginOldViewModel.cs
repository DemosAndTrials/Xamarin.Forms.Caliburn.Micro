using System;
using Caliburn.Micro;
using PropertyChanged;
using Shared.Services;

namespace Shared.ViewModels
{
    public class LoginOldViewModel : Screen
    {
        private readonly ICredentialsService _credentials;
        private readonly IAuthenticationService _authentication;
        private readonly IApplicationNavigationService _navigation;

        public LoginOldViewModel(ICredentialsService credentials, IAuthenticationService authentication, IApplicationNavigationService navigation)
        {
            this._credentials = credentials;
            this._authentication = authentication;
            this._navigation = navigation;
        }

        protected override async void OnInitialize()
        {
            var stored = await _credentials.GetCredentialsAsync();

            if (stored == Credentials.None)
                return;

            Account = stored.Account;
            Token = stored.Token;
        }

        public string Account { get; set; }

        public string Token { get; set; }

        public string Message { get; private set; }

        [DependsOn(nameof(Account), nameof(Token))]
        public bool CanLogin => !String.IsNullOrEmpty(Account) && !String.IsNullOrEmpty(Token);

        public async void Login()
        {
            var entered = new Credentials(Account, Token);
            var authenticated = await _authentication.AuthenticateCredentialsAsync(entered);

            if (!authenticated)
            {
                Message = "Account / Token is incorrect";
            }
            else
            {
                await _credentials.StoreAsync(entered);

                _navigation.ToProjects();
            }
        }
    }
}
