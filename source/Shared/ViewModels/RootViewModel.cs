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


        #endregion

        #region Constructor

        public RootViewModel(IApplicationNavigationService navigation)
        {
            _navigation = navigation;
        }

        #endregion

        #region Methods

        protected override void OnActivate()
        {
            base.OnActivate();

            if (NeedsLogin())
            {
                _navigation.To<MainViewModel>();

            }
        }

        private bool NeedsLogin()
        {
            try
            {
                
                return true;
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
