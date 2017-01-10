using System.Threading.Tasks;
using Caliburn.Micro;
using Shared.Services;

namespace Shared.ViewModels
{
    public class MainViewModel : Screen
    {
        #region Fields and Properties

        private readonly IApplicationNavigationService _navigation;
        private readonly IDialogService _dialogs;
        private readonly IAuthenticationService _authentication;

        public string DialogMessage { get; private set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value) return;
                _username = value; NotifyOfPropertyChange();
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (_emailAddress == value) return;
                _emailAddress = value; NotifyOfPropertyChange();
            }
        }

        #endregion

        #region Constructor

        public MainViewModel(IApplicationNavigationService navigation, IDialogService dialogs, IAuthenticationService authentication)
        {
            _navigation = navigation;
            _dialogs = dialogs;
            _authentication = authentication;
        }

        #endregion

        #region Overrides

        public override string DisplayName
        {
            get { return "MainView"; }
            set { }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (_authentication.UserAccount != null)
            {
                Username = _authentication.UserAccount.Username;
                EmailAddress = _authentication.UserAccount.Properties[Constants.EmailAccountProperty];
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            //release resource here
        }

        #endregion

        #region Methods

        public void Login()
        {
            _navigation.To<LoginViewModel>();
        }

        public async Task Popup()
        {
            var answer = await _dialogs.ShowDialogAsync("Question?", "Would you like to play a game", "Yes", "No");
            var msg = "You clicked ";
            msg += answer ? "Yes" : "No";
            DialogMessage = msg;
        } 

        #endregion
    }
}
