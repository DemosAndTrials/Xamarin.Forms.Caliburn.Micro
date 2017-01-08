using System;
using Caliburn.Micro;
using Shared.Services;

namespace Shared.ViewModels
{
    public class LegacyLoginViewModel : Screen
    {
        private readonly ICredentialsService _credentials;
        private readonly IAuthenticationService _authentication;
        private readonly IApplicationNavigationService _navigation;

        private string _account;
        private string _token;
        private string _message;

        public LegacyLoginViewModel(ICredentialsService credentials, IAuthenticationService authentication, IApplicationNavigationService navigation)
        {
            _credentials = credentials;
            _authentication = authentication;
            _navigation = navigation;
        }

        protected override async void OnInitialize()
        {
            var stored = await _credentials.GetCredentialsAsync();

            if (stored == Credentials.None)
                return;

            Account = stored.Account;
            Token = stored.Token;
        }

        public string Account
        {
            get { return _account; }
            set
            {
                _account = value;
                NotifyOfPropertyChange(() => Account);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                NotifyOfPropertyChange(() => Token);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public bool CanLogin
        {
            get { return !String.IsNullOrEmpty(Account) && !String.IsNullOrEmpty(Token); }
        }

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
