using System;
using System.Diagnostics;
using Caliburn.Micro;
using Shared.Services;

namespace Shared.ViewModels
{
    public class RootViewModel : Conductor<IScreen>
    {
        #region Field and Properties

        private readonly IApplicationNavigationService _navigation;
        private readonly IAuthenticationService _authentication;

        #endregion

        #region Constructor

        public RootViewModel(IApplicationNavigationService navigation, IAuthenticationService authentication)
        {
            _navigation = navigation;
            _authentication = authentication;
        }

        #endregion

        #region Methods

        protected override void OnActivate()
        {
            base.OnActivate();

            if (NeedsLogin())
            {
                _navigation.To<LoginViewModel>();
            }
            else
            {
                _navigation.To<MainViewModel>();
            }
        }

        private bool NeedsLogin()
        {
            try
            {
                // check if user authenticated here
                return _authentication.UserAccount == null;
            }
            catch (Exception e)
            {
                // needs logger
                Debug.WriteLine(e);
            }
            return true;
        }

        #endregion
    }
}
