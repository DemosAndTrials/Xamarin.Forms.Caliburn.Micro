using Caliburn.Micro;
using Shared.Services;

namespace Shared.ViewModels
{
    /// <summary>
    /// Login ViewModel
    /// @created  	    10/01/2017
    /// @lastModified  	10/01/2017
    /// - init
    /// </summary>
    public sealed class LoginViewModel : Screen
    {
        #region Fields and Properties

        private readonly IApplicationNavigationService _navigation;

        #endregion

        #region Constructor

        public LoginViewModel(IApplicationNavigationService navigation)
        {
            _navigation = navigation;
        }

        #endregion

        #region Methods

        public void Login(string orgType)
        {
            _navigation.To<LoginPopupViewModel>(orgType);
        }

        #endregion

        #region Overrides

        public override string DisplayName
        {
            get { return "LoginView"; }
            set { }
        }

        #endregion
    }
}
